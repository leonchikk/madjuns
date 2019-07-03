using Authentication.Interfaces;
using Authentication.Models.Requests;
using Authentication.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationToken Login(AuthenticationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
