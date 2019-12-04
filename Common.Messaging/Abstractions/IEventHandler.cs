using Common.Messaging.Events;
using System.Threading.Tasks;

namespace Common.Messaging.Abstractions
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task HandleAsync(TEvent @event);
    }
}
