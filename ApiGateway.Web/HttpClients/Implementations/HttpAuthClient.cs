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
    public class HttpAuthClient : BaseClient, IHttpAuthClient
    {
        private readonly string _clientName = "auth";
        private readonly IOptionsMonitor<GatewaySettings> _gatewaySettings;

        public HttpAuthClient(IHttpBaseClient httpBaseClient, IHttpContextAccessor httpAccessor, IOptionsMonitor<GatewaySettings> gatewaySettings) : base(httpBaseClient, httpAccessor)
        {
            _gatewaySettings = gatewaySettings;
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            await HttpClient.PostAsync(_clientName, "api/auth/forgot-password", requestModel, Headers);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestModel requestModel)
        {
            await HttpClient.PostAsync(_clientName, "api/auth/reset-password", requestModel, Headers);
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel requestModel)
        {
            return await HttpClient.PostAsync<SignInResponseModel, SignInRequestModel>(_clientName, "api/auth/sign-in", requestModel, Headers);
        }

        public async Task SignUpAsync(SignUpRequestModel requestModel)
        {
            await HttpClient.PostAsync(_clientName, "api/auth/sign-up", requestModel, Headers);
        }

        public async Task<VerifyEmailResponseModel> VerifyEmailAsync(VerifyEmailRequestModel requestModel)
        {
            var url = $"{_gatewaySettings.CurrentValue.AuthApiUrl}api/auth/verify-email".AddUrlParameters(requestModel);

            return await HttpClient.GetAsync<VerifyEmailResponseModel>(_clientName, url, Headers);
        }
    }
}
