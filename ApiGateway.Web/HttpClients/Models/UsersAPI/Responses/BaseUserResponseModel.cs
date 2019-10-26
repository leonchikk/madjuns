using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Models.UsersAPI.Responses
{
    public class BaseUserResponseModel
    {
        public Guid UserId { get; set; }
        public ProfileResponseModel Profile { get; set; }
    }
}
