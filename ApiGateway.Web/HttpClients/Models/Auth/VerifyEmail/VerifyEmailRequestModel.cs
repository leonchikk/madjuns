namespace ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail
{
    public class VerifyEmailRequestModel
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
    }
}
