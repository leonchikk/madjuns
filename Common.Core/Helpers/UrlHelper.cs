using System;
using System.Collections.Generic;
using System.Web;

namespace Common.Core.Helpers
{
    public static class UrlHelper
    {
        public static string AddUrlParameters(string url, IDictionary<string, string> parameters)
        {
            UriBuilder uriBuilder = new UriBuilder(url);
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                query[parameter.Key] = parameter.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
