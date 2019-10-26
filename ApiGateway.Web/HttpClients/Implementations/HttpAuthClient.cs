using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using Common.Networking.Helpers;
using Common.Networking.Implementaions;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpAuthClient : BaseClient, IHttpAuthClient
    {
        public HttpAuthClient(IHttpBaseClient httpBaseClient, IHttpContextAccessor httpAccessor): base(httpBaseClient, httpAccessor) { }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            await HttpClient.PostAsync("auth", "api/auth/forgot-password", requestModel, Headers);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestModel requestModel)
        {
            await HttpClient.PostAsync("auth", "api/auth/reset-password", requestModel, Headers);
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel)
        {
            return await HttpClient.PostAsync<SignInResponseModel, SignInRequestModel>("auth", "api/auth/sign-in", requestModel, Headers);
        }

        public async Task SignUpAsync(SignUpRequestModel requestModel)
        {
            await HttpClient.PostAsync("auth", "api/auth/sign-up", requestModel, Headers);
        }

        public async Task<VerifyEmailResponseModel> VerifyEmailAsync(string serviceUrl, VerifyEmailRequestModel requestModel)
        {
            var url = $"{serviceUrl}api/auth/verify-email".AddUrlParameters(requestModel);

            return await HttpClient.GetAsync<VerifyEmailResponseModel>("auth", url, Headers);
        }
    }
}
