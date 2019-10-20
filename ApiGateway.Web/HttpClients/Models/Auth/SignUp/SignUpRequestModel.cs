using System;

namespace ApiGateway.Web.HttpClients.Models.Auth.SignUp
{
    public class SignUpRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public string RedirectUrl { get; set; }
    }
}
