using System;
using System.Collections.Generic;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IBaseClient
    {
        Dictionary<string, string> Headers { get; set; }
    }
}
