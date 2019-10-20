namespace ApiGateway.Web.HttpClients.Models.Auth.ResetPassword
{
    public class ResetPasswordRequestModel
    {
        public string Password { get; set; }
        public string ForgotPasswordToken { get; set; }
    }
}
