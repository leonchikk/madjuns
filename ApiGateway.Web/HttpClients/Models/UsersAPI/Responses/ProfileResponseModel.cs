﻿using System;

namespace ApiGateway.Web.HttpClients.Models.UsersAPI.Responses
{
    public class ProfileResponseModel
    {
        public AddressResponseModel Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DayOfBirth { get; set; }
    }
}