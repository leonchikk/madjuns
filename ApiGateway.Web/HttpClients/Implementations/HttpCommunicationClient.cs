using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.CommunicationAPI.RequestModels;
using ApiGateway.Web.HttpClients.Models.CommunicationAPI.ViewModels;
using Common.Networking.Interfaces;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpCommunicationClient : IHttpCommunicationClient
    {
        private readonly string _clientName = "communication";
        private readonly IHttpBaseClient _httpBaseClient;

        public HttpCommunicationClient(IHttpBaseClient httpBaseClient)
        {
            _httpBaseClient = httpBaseClient;
        }

        public async Task<ChannelViewModel> CreateChannelAsync(CreateChannelRequest request)
        {
            return await _httpBaseClient.PostAsync<ChannelViewModel, CreateChannelRequest>(_clientName, $"api/channels/create", request);
        }
    }
}
