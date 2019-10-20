using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public static ByteArrayContent GenerateHttpContent<TRequest>(this TRequest requestModel, Dictionary<string, string> headers, string mediaType) where TRequest: class
        {
            var jsonFromRequestModel = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonFromRequestModel);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    byteContent.Headers.Add(header.Key, header.Value);
                }
            }

            return byteContent;
        }
    }
}
