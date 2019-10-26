
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Responses;
using Common.Core.SearchFilters.Pagination;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpUsersClient
    {
        Task<BaseListResponseModel<BaseUserResponseModel>> GetUserBlackList(string serviceUrl, BansSimpleSearchModel searchModel);
        Task<BaseListResponseModel<BaseUserResponseModel>> GetUsers(string serviceUrl, UsersSimpleSearchModel searchModel);
    }
}
