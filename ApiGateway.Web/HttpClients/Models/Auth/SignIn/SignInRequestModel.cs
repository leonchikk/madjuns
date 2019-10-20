using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Models.Auth
{
    public class SignInRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
