using System;

namespace Auth.Models.Requests
{
    public class CreateAccountRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
