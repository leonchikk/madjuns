using Authentication.Data.Entities;
using Authentication.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        AuthenticationToken CreateToken(User user);
    }
}
