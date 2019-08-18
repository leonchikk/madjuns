namespace Auth.API.Models.Requests
{
    public class VerifyEmailRequest
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
    }
}
