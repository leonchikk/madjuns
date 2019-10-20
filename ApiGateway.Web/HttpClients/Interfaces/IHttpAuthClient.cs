

using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpAuthClient
    {
        Task SignUpAsync(SignUpRequestModel requestModel);
        Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel);

        Task<VerifyEmailResponseModel> VerifyEmailAsync(string hostUrl, VerifyEmailRequestModel requestModel);
        Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel);
        Task ResetPasswordAsync(ResetPasswordRequestModel requestModel);

    }
}
