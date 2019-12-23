using AutoMapper;
using Common.Core.Interfaces;
using Common.Messaging.Abstractions;

namespace Users.Services
{
    public interface IBaseService
    {
        IUnitOfWork UnitOfWork { get; set; }
        IEventBus ServiceBus { get; set; }
    }
}
