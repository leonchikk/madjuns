using Authentication.Models.Requests;
using Authentication.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationToken Login(AuthenticationRequest request);
    }
}
