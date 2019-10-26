using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Responses;
using Common.Core.SearchFilters.Pagination;
using Common.Networking.Helpers;
using Common.Networking.Implementaions;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpUsersClient : BaseClient, IHttpUsersClient
    {
        public HttpUsersClient(IHttpBaseClient httpBaseClient, IHttpContextAccessor httpAccessor): base(httpBaseClient, httpAccessor) { }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUserBlackList(string serviceUrl, BansSimpleSearchModel searchModel)
        {
            var url = $"{serviceUrl}api/bans/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>("users", url, Headers);
        }

        public async Task<BaseListResponseModel<BaseUserResponseModel>> GetUsers(string serviceUrl, UsersSimpleSearchModel searchModel)
        {
            var url = $"{serviceUrl}api/users/".AddUrlParameters(searchModel);
            return await HttpClient.GetAsync<BaseListResponseModel<BaseUserResponseModel>>("users", url, Headers);
        }
    }
}
