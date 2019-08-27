using System.Threading.Tasks;

namespace Common.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
