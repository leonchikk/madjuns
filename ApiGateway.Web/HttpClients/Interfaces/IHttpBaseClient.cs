using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpBaseClient
    {
        Task<TResult> SendPostAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, string mediaType = "application/json");
    }
}
