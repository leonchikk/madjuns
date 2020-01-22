﻿using Newtonsoft.Json;
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

        public static HttpClient SetAuthorizationHeader(this HttpClient client, Dictionary<string, string> headers = null)
        {
            if (headers != null && headers.ContainsKey("Authorization"))
            {
                // Remove bearer work from start
                var token = headers["Authorization"].Replace("Bearer ", string.Empty);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public static ByteArrayContent GenerateHttpContent<TRequest>(this TRequest requestModel, Dictionary<string, string> headers, string mediaType) where TRequest : class
        {
            var jsonFromRequestModel = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonFromRequestModel);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (header.Key == "Authorization")
                    {
                        continue;
                    }

                    byteContent.Headers.Add(header.Key, header.Value);
                }
            }

            return byteContent;
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
