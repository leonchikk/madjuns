using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Services.Models.Responses;

namespace Users.Services.Services.Bans
{
    public interface IBansService : IBaseService
    {
        IQueryable<BlockedUser> GetUserBlackList(Guid userId);
        Task<User> AddToBlackListAsync(Guid currentUserId, Guid targetUserId);
        Task RemoveFromBlackList(Guid currentUserId, Guid targetUserId);
    }
}
