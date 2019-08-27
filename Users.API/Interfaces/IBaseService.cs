using AutoMapper;
using Common.Core.Interfaces;
using EasyNetQ;

namespace Users.API.Interfaces
{
    public interface IBaseService
    {
        IUnitOfWork UnitOfWork { get; set; }
        IBus ServiceBus { get; set; }
        IMapper Mapper { get; set; }
    }
}
