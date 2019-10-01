using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Services.Models.Responses;

namespace Users.Services.Services.Bans
{
    public interface IBansService: IBaseService
    {
        IEnumerable<BaseUserResponseModel> GetUserBlackList(Guid userId);
        Task<UserResponseModel> AddToBlackListAsync(Guid currentUserId, Guid targetUserId);
        Task RemoveFromBlackList(Guid currentUserId, Guid targetUserId);
    }
}
