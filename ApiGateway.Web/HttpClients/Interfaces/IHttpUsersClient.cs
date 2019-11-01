
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Responses;
using Common.Core.SearchFilters.Pagination;
using System;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpUsersClient
    {
        Task<BaseListResponseModel<BaseUserResponseModel>> GetUserBlackListAsync(BansSimpleSearchModel searchModel);
        Task<BaseUserResponseModel> AddUserToBlackListAsync(Guid targetUserId);
        Task RemoveFromBlackListAsync(Guid bannedUserId);


        Task<BaseListResponseModel<BaseUserResponseModel>> GetUserFriendsAsync(FriendsSimpleSearchModel searchModel);
        Task<BaseUserResponseModel> AddToFriendAsync(Guid subscriberId);
        Task RemoveFriendAsync(Guid friendId);


        Task<BaseListResponseModel<BaseUserResponseModel>> GetUserSubscribersAsync(SubscribersSimpleSearchModel searchModel);
        Task<BaseUserResponseModel> SendRequestToBeFriendAsync(Guid userId);
        Task RejectSubscriptionAsync(Guid userId);


        Task<BaseListResponseModel<BaseUserResponseModel>> GetUsersAsync(UsersSimpleSearchModel searchModel);
        Task<UserResponseModel> GetUserByIdAsync(Guid userId);
        Task<ProfileResponseModel> GetUserProfileByIdAsync(Guid userId);
        Task<SettingResponseModel> GetUserSettingsByIdAsync(Guid userId);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task DeleteUserAsync(Guid id);
    }
}
