using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using Common.Networking.Extensions;
using Common.Networking.Helpers;
using Common.Networking.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpAuthClient : IHttpAuthClient
    {
        private readonly IHttpBaseClient _httpBaseClient;

        public HttpAuthClient(IHttpBaseClient httpBaseClient)
        {
            _httpBaseClient = httpBaseClient;
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/forgot-password", requestModel);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/reset-password", requestModel);
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel)
        {
            return await _httpBaseClient.PostAsync<SignInResponseModel, SignInRequestModel>("auth", "api/auth/sign-in", requestModel);
        }

        public async Task SignUpAsync(SignUpRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/sign-up", requestModel);
        }

        public async Task<VerifyEmailResponseModel> VerifyEmailAsync(string hostUrl, VerifyEmailRequestModel requestModel)
        {
            var url = $"{hostUrl}api/auth/verify-email".AddUrlParameters(requestModel);

            return await _httpBaseClient.GetAsync<VerifyEmailResponseModel>("auth", url);
        }
    }
}
