using Common.Networking.Extensions;
using System;
using System.Collections.Generic;
using System.Web;

namespace Common.Networking.Helpers
{
    public static class UriBuilderExtensions
    {
        public static string AddUrlParameters(this string url, IDictionary<string, string> parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var parameter in parameters)
                query[parameter.Key] = parameter.Value;

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        public static string AddUrlParameters<TModel>(this string url, TModel model) where TModel: class
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            var parameters = model.ToDictionary();

            foreach (var parameter in parameters)
                query[parameter.Key] = parameter.Value.ToString();

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
