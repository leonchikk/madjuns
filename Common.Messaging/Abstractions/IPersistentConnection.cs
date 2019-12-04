using RabbitMQ.Client;
using System;

namespace Common.Messaging.Abstractions
{
    public interface IPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
