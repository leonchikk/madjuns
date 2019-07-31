using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.Requests
{
    public class VerifyEmailRequest
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
    }
}
