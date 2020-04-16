using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using ApiGateway.Web.Settings;
using Common.Networking.Helpers;
using Common.Networking.Implementaions;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpAuthClient :  IHttpAuthClient
    {
        private readonly string _clientName = "auth";
        private readonly IHttpBaseClient _httpBaseClient;

        public HttpAuthClient(IHttpBaseClient httpBaseClient) 
        {
            _httpBaseClient = httpBaseClient;
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync(_clientName, "api/auth/forgot-password", requestModel);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync(_clientName, "api/auth/reset-password", requestModel);
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel)
        {
            return await _httpBaseClient.PostAsync<SignInResponseModel, SignInRequestModel>(_clientName, "api/auth/sign-in", requestModel);
        }

        public async Task SignUpAsync(SignUpRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync(_clientName, "api/auth/sign-up", requestModel);
        }

        public async Task<VerifyEmailResponseModel> VerifyEmailAsync(VerifyEmailRequestModel requestModel)
        {
            var url = $"api/auth/verify-email".AddUrlParameters(requestModel);

            return await _httpBaseClient.GetAsync<VerifyEmailResponseModel>(_clientName, url);
        }
    }
}
