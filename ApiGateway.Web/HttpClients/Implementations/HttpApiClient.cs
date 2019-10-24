using System.Collections.Generic;
using ApiGateway.Web.HttpClients.Interfaces;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpApiClient : IHttpApiClient
    {
        public Dictionary<string, string> Headers { get; set; }
    }
}
