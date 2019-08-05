using Auth.Core.Entities;
using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.ExternalProviders.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthorizationToken CreateToken(Account account)
        {
            ClaimsIdentity identity = GetIdentity(account);

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Authentication:Issuer").Value,
                    audience: _configuration.GetSection("Authentication:Audience").Value,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(_configuration.GetSection("Authentication:LifeTime").Value))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:Key").Value)),
                                                                                                                            SecurityAlgorithms.HmacSha256));

            string encodedJwt = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthorizationToken
            {
                Token = encodedJwt
            };
        }

        private static ClaimsIdentity GetIdentity(Account account)
        {
            List<Claim> claims = new List<Claim>
                {
                   new Claim("UserId", account.Id.ToString())
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
