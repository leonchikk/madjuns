﻿using System;

namespace Users.API.Models.Responses
{
    public class ProfileResponseModel
    {
        public AddressResponseModel Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DayOfBirth { get; set; }
    }
}
