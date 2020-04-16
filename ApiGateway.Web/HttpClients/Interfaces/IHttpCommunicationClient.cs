using ApiGateway.Web.HttpClients.Models.CommunicationAPI.RequestModels;
using ApiGateway.Web.HttpClients.Models.CommunicationAPI.ViewModels;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpCommunicationClient
    {
        Task<ChannelViewModel> CreateChannelAsync(CreateChannelRequest request);
    }
}
