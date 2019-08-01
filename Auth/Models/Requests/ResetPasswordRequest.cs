using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string ForgotPasswordToken { get; set; }
    }
}
