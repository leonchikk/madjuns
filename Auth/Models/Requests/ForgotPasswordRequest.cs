using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.Requests
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }
}
