namespace ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword
{
    public class ForgotPasswordRequestModel
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }
}
