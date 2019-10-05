using Auth.Core.Entities;
using Authentication.API.Interfaces;
using Authentication.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationToken CreateToken(Account account)
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

            return new AuthenticationToken
            {
                Token = encodedJwt
            };
        }

        private static ClaimsIdentity GetIdentity(Account account)
        {
            List<Claim> claims = new List<Claim>
                {
                   new Claim("AccountId", account.Id.ToString()),
                   new Claim("SystemRole", account.SystemRole.ToString()),
                   new Claim("Email", account.Email)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
