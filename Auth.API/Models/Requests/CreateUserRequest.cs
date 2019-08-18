using System;

namespace Auth.API.Models.Requests
{
    public class CreateAccountRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public string RedirectUrl { get; set; }
    }
}
