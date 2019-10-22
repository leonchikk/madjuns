using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using Common.Networking.Helpers;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpAuthClient : IHttpAuthClient
    {
        private readonly IHttpBaseClient _httpBaseClient;
        private readonly IHttpContextAccessor _httpAccessor;
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public HttpAuthClient(IHttpBaseClient httpBaseClient, IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
            _httpBaseClient = httpBaseClient;

            var request = _httpAccessor.HttpContext.Request;
            Headers.Add("X-ApiGateway-Address", $"{request.Scheme}://{request.Host}");
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/forgot-password", requestModel, Headers);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/reset-password", requestModel, Headers);
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel)
        {
            return await _httpBaseClient.PostAsync<SignInResponseModel, SignInRequestModel>("auth", "api/auth/sign-in", requestModel, Headers);
        }

        public async Task SignUpAsync(SignUpRequestModel requestModel)
        {
            await _httpBaseClient.PostAsync("auth", "api/auth/sign-up", requestModel, Headers);
        }

        public async Task<VerifyEmailResponseModel> VerifyEmailAsync(string hostUrl, VerifyEmailRequestModel requestModel)
        {
            var url = $"{hostUrl}api/auth/verify-email".AddUrlParameters(requestModel);

            return await _httpBaseClient.GetAsync<VerifyEmailResponseModel>("auth", url, Headers);
        }
    }
}
