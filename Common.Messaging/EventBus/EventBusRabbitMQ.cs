using Common.Messaging.Abstractions;
using Common.Messaging.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messaging.EventBus
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string BROKER_NAME = "app.events.exchange";
        private const string BROKER_ALT_NAME = "app.events.exchange.alt";
        private const string DLX_BROKER_NAME = "app.events.dlx";
        private const string RETRY_QUEUE = "retry.queue";
        private const string RETRY_ALT_QUEUE = "retry.queue.alt";
        private const int RETRY_DELAY = 10_000; // in ms

        private readonly IPersistentConnection _persistentConnection;
        private readonly ISubscriptionsManager _subscriptionsManager;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retryCount;
        private readonly int _resendMessageCount;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ
        (
            IServiceProvider serviceProvider,
            ILogger<EventBusRabbitMQ> logger,
            IPersistentConnection persistentConnection,
            ISubscriptionsManager subscriptionsManager,
            string queueName,
            int retryCount = 10,
            int resendMessageCount = 10
        )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _persistentConnection = persistentConnection;
            _subscriptionsManager = subscriptionsManager;
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _retryCount = retryCount;
            _resendMessageCount = resendMessageCount;

            _subscriptionsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);

                if (_subscriptionsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Publish(Event @event)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogError(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            using (var channel = _persistentConnection.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                channel.ExchangeDeclare(
                exchange: BROKER_ALT_NAME,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);

                channel.QueueDeclare(
                    queue: RETRY_ALT_QUEUE,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);

                var exchangeArgs = new Dictionary<string, object>
                {
                    { "alternate-exchange", BROKER_ALT_NAME }
                };

                channel.ExchangeDeclare(
                    exchange: BROKER_NAME,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false,
                    arguments: exchangeArgs);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent
                    properties.CorrelationId = Guid.NewGuid().ToString();

                    _logger.LogTrace($"Publishing event to RabbitMQ: {@event.Id}");

                    channel.BasicPublish(
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = _subscriptionsManager.GetEventKey<TEvent>();
            DoInternalSubscription<TEvent>();

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TEventHandler));

            _subscriptionsManager.AddSubscription<TEvent, TEventHandler>();
            StartBasicConsume();
        }

        private void DoInternalSubscription<TEvent>() where TEvent : Event
        {
            var eventName = _subscriptionsManager.GetEventKey<TEvent>();
            var containsKey = _subscriptionsManager.HasSubscriptions<TEvent>();
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    var args = new Dictionary<string, object>();
                    args.Add("x-dead-letter-exchange", DLX_BROKER_NAME);

                    channel.QueueDeclare(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: args);

                    channel.QueueBind(
                        queue: _queueName,
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        args);

                    channel.QueueBind(RETRY_QUEUE, DLX_BROKER_NAME, eventName);
                    channel.QueueBind(RETRY_ALT_QUEUE, BROKER_ALT_NAME, eventName);
                }
            }
        }

        public void RemoveSubscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = _subscriptionsManager.GetEventKey<TEvent>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subscriptionsManager.RemoveSubscription<TEvent, TEventHandler>();
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subscriptionsManager.Clear();
        }

        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;

            if (eventArgs.BasicProperties.IsHeadersPresent() && eventArgs.BasicProperties.Headers.ContainsKey("x-death"))
            {
                var deathHeaderGroups = eventArgs.BasicProperties.Headers["x-death"] as List<object>;
                var deathHeaders = deathHeaderGroups.FirstOrDefault() as Dictionary<string, object>;
                var count = Convert.ToInt32(deathHeaders["count"]);

                if (count > _resendMessageCount)
                {
                    PublishToAltQueue(eventArgs);
                    _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                    return;
                }
            }

            var message = Encoding.UTF8.GetString(eventArgs.Body);

            try
            {
                var processed = await ProcessEvent(eventName, message);

                if (processed)
                    _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                else
                    _consumerChannel.BasicReject(eventArgs.DeliveryTag, requeue: false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);

                _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
        }

        private void PublishToAltQueue(BasicDeliverEventArgs @event)
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, $"Could not publish dead event with correlationId {@event.BasicProperties.CorrelationId} after {time.TotalSeconds:n1}s ({ex.Message})");
                });

            policy.Execute(() =>
            {
                _logger.LogTrace($"Publishing event to Alternative DLX: {@event.RoutingKey}");

                _consumerChannel.BasicPublish(
                    exchange: BROKER_ALT_NAME,
                    routingKey: @event.RoutingKey,
                    mandatory: true,
                    basicProperties: @event.BasicProperties,
                    body: @event.Body);
            });
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(
                exchange: BROKER_ALT_NAME,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);

            channel.QueueDeclare(
                queue: RETRY_ALT_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var exchangeArgs = new Dictionary<string, object>
            {
                { "alternate-exchange", BROKER_ALT_NAME }
            };

            channel.ExchangeDeclare(
                exchange: BROKER_NAME,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: exchangeArgs);

            channel.ExchangeDeclare(
                exchange: DLX_BROKER_NAME,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);

            var queueArgs = new Dictionary<string, object>();
            queueArgs.Add("x-dead-letter-exchange", BROKER_NAME);
            queueArgs.Add("x-message-ttl", RETRY_DELAY);

            channel.QueueDeclare(
                queue: RETRY_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: queueArgs);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            if (_subscriptionsManager.HasSubscriptions(eventName))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await Task.Yield();
                        await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
                
                return false;
            }

            return true;
        }
    }
}
