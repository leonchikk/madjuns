using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Communication.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _context;

        public TokenService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetUserId()
        {
            var idClaim = _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId");

            if (idClaim == null)
                return Guid.Empty;

            return Guid.Parse(idClaim.Value);
        }
    }
}
