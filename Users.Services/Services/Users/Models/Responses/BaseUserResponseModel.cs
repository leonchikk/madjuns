using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Services.Users.Models.Responses
{
    public class BaseUserResponseModel
    {
        public Guid UserId { get; set; }
        public ProfileResponseModel Profile { get; set; }
    }
}
