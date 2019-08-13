using AutoMapper;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Interfaces;

namespace Users.API.Interfaces
{
    public interface IBaseService
    {
        IUnitOfWork UnitOfWork { get; set; }
        IBus ServiceBus { get; set; }
        IMapper Mapper { get; set; }
    }
}
