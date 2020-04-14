using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Networking.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<TResult> HandleResponseAsync<TResult>(this HttpResponseMessage httpResponseMessage)
        {
            var resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(resultContent);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception(resultContent);
            }

            return JsonConvert.DeserializeObject<TResult>(resultContent);
        }

        public static async Task HandleResponseAsync(this HttpResponseMessage httpResponseMessage)
        {
            var resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(resultContent);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception(resultContent);
            }
        }

        public static StringContent ToContent<TRequest>(this TRequest requestModel, string mediaType) where TRequest : class
        {
            var jsonFromRequestModel = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonFromRequestModel, Encoding.UTF8, mediaType);

            return content;
        }

        public static void CopyHeaders(this HttpRequestMessage requestMessage, Dictionary<string, string> headersToCopy)
        {
            if (headersToCopy == null)
                return;

            foreach (var header in headersToCopy)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
         }
    }
}
