using System;
using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Responses;
using ApiGateway.Web.Settings;
using Common.Core.SearchFilters.Pagination;
using Common.Networking.Helpers;
using Common.Networking.Implementaions;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpUsersClient : BaseClient, IHttpUsersClient
    {
        private readonly string _clientName = "users";
        private readonly IOptionsMonitor<GatewaySettings> _gatewaySettings;

        public HttpUsersClient(IHttpBaseClient httpBaseClient, IHttpContextAccessor httpAccessor, IOptionsMonitor<GatewaySettings> gatewaySettings) : base(httpBaseClient, httpAccessor)
        {
            _gatewaySettings = gatewaySettings;
            Headers.Add("Authorization", HttpContext.Request.Headers["Authorization"]);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserBlackListAsync(BansSimpleSearchModel searchModel)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/bans/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url, Headers);
        }

        public async Task<BaseUserResponseModel> AddUserToBlackListAsync(Guid targetUserId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/bans/add/{targetUserId.ToString()}";
            return await HttpClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null, Headers);
        }

        public async Task RemoveFromBlackListAsync(Guid bannedUserId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/bans/{bannedUserId.ToString()}";
            await HttpClient.DeleteAsync(_clientName, url, Headers);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/{id.ToString()}";
            await HttpClient.DeleteAsync(_clientName, url, Headers);
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid userId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/{userId.ToString()}";
            return await HttpClient.GetAsync<UserResponseModel>(_clientName, url, Headers);
        }

        public async Task<ProfileResponseModel> GetUserProfileByIdAsync(Guid userId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/{userId.ToString()}/profile";
            return await HttpClient.GetAsync<ProfileResponseModel>(_clientName, url, Headers);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUsersAsync(UsersSimpleSearchModel searchModel)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url, Headers);
        }

        public async Task<SettingResponseModel> GetUserSettingsByIdAsync(Guid userId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/{userId.ToString()}/setting";
            return await HttpClient.GetAsync<SettingResponseModel>(_clientName, url, Headers);
        }

        public async Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/users/{id.ToString()}";
            return await HttpClient.PutAsync<UserResponseModel, UpdateUserRequest>(_clientName, url, request, Headers);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserFriendsAsync(FriendsSimpleSearchModel searchModel)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/friends/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url, Headers);
        }

        public async Task<BaseUserResponseModel> AddToFriendAsync(Guid subscriberId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/friends/add/{subscriberId.ToString()}";
            return await HttpClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null, Headers);
        }

        public async Task RemoveFriendAsync(Guid friendId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/friends/{friendId.ToString()}";
            await HttpClient.DeleteAsync(_clientName, url, Headers);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserSubscribersAsync(SubscribersSimpleSearchModel searchModel)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/subscribers/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url, Headers);
        }

        public async Task<BaseUserResponseModel> SendRequestToBeFriendAsync(Guid userId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/subscribers/subscribe-to/{userId.ToString()}";
            return await HttpClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null, Headers);
        }

        public async Task RejectSubscriptionAsync(Guid userId)
        {
            var url = $"{_gatewaySettings.CurrentValue.UsersApiUrl}api/subscribers/reject/{userId.ToString()}";
            await HttpClient.DeleteAsync(_clientName, url, Headers);
        }
    }
}
