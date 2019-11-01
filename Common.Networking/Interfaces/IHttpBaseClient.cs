using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Networking.Interfaces
{
    public interface IHttpBaseClient
    {
        Task<TResult> GetAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = default);

        Task DeleteAsync(string clientName, string requestUri, Dictionary<string, string> headers = default);
        Task<TResult> DeleteAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = default);

        Task PostAsync<TRequest>(string clientName,  string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class;
        Task<TResult> PostAsync<TResult, TRequest>(string clientName,  string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class;

        Task PutAsync<TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class;
        Task<TResult> PutAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class;
    }
}
