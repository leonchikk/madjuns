namespace Auth.API.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string ForgotPasswordToken { get; set; }
    }
}
