﻿using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Responses;
using ApiGateway.Web.Settings;
using Common.Core.SearchFilters.Pagination;
using Common.Networking.Helpers;
using Common.Networking.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpUsersClient : IHttpUsersClient
    {
        private readonly string _clientName = "users";
        private readonly IHttpBaseClient _httpBaseClient;

        public HttpUsersClient(IHttpBaseClient httpBaseClient)
        {
            _httpBaseClient = httpBaseClient;
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserBlackListAsync(BansSimpleSearchModel searchModel)
        {
            var url = $"api/bans/".AddUrlParameters(searchModel);
            return await _httpBaseClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url);
        }

        public async Task<BaseUserResponseModel> AddUserToBlackListAsync(Guid targetUserId)
        {
            var url = $"api/bans/add/{targetUserId.ToString()}";
            return await _httpBaseClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null);
        }

        public async Task RemoveFromBlackListAsync(Guid bannedUserId)
        {
            var url = $"api/bans/{bannedUserId.ToString()}";
            await _httpBaseClient.DeleteAsync(_clientName, url);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var url = $"api/users/{id.ToString()}";
            await _httpBaseClient.DeleteAsync(_clientName, url);
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid userId)
        {
            var url = $"api/users/{userId.ToString()}";
            return await _httpBaseClient.GetAsync<UserResponseModel>(_clientName, url);
        }

        public async Task<ProfileResponseModel> GetUserProfileByIdAsync(Guid userId)
        {
            var url = $"api/users/{userId.ToString()}/profile";
            return await _httpBaseClient.GetAsync<ProfileResponseModel>(_clientName, url);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUsersAsync(UsersSimpleSearchModel searchModel)
        {
            var url = $"api/users/".AddUrlParameters(searchModel);
            return await _httpBaseClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url);
        }

        public async Task<SettingResponseModel> GetUserSettingsByIdAsync(Guid userId)
        {
            var url = $"api/users/{userId.ToString()}/setting";
            return await _httpBaseClient.GetAsync<SettingResponseModel>(_clientName, url);
        }

        public async Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var url = $"api/users/{id.ToString()}";
            return await _httpBaseClient.PutAsync<UserResponseModel, UpdateUserRequest>(_clientName, url, request);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserFriendsAsync(FriendsSimpleSearchModel searchModel)
        {
            var url = $"api/friends/".AddUrlParameters(searchModel);
            return await _httpBaseClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url);
        }

        public async Task<BaseUserResponseModel> AddToFriendAsync(Guid subscriberId)
        {
            var url = $"api/friends/add/{subscriberId.ToString()}";
            return await _httpBaseClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null);
        }

        public async Task RemoveFriendAsync(Guid friendId)
        {
            var url = $"api/friends/{friendId.ToString()}";
            await _httpBaseClient.DeleteAsync(_clientName, url);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserSubscribersAsync(SubscribersSimpleSearchModel searchModel)
        {
            var url = $"api/subscribers/".AddUrlParameters(searchModel);
            return await _httpBaseClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>(_clientName, url);
        }

        public async Task<BaseUserResponseModel> SendRequestToBeFriendAsync(Guid userId)
        {
            var url = $"api/subscribers/subscribe-to/{userId.ToString()}";
            return await _httpBaseClient.PutAsync<BaseUserResponseModel, BansSimpleSearchModel>(_clientName, url, null);
        }

        public async Task RejectSubscriptionAsync(Guid userId)
        {
            var url = $"api/subscribers/reject/{userId.ToString()}";
            await _httpBaseClient.DeleteAsync(_clientName, url);
        }
    }
}
