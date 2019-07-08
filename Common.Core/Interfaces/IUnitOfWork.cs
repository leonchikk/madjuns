using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        Task SaveAsync();
    }
}
