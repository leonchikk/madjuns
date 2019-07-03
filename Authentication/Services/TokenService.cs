using Authentication.Data.Models;
using Authentication.Interfaces;
using Authentication.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class TokenService : ITokenService
    {
        public AuthenticationToken CreateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
