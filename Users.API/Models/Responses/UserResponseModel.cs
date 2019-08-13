﻿using System;

namespace Users.API.Models.Responses
{
    public class UserResponseModel
    {
        public Guid UserId { get; set; }
        public ProfileResponseModel Profile { get; set; }
        public SettingResponseModel[] Settings { get; set; }
        public UserResponseModel[] Friends { get; set; }
        public UserResponseModel[] Subscribers { get; set; }
        public UserResponseModel[] BlackList { get; set; }
    }
}
