using System;

namespace Users.Services.Users.Models.Responses
{
    public class UserResponseModel: BaseUserResponseModel
    {
        public SettingResponseModel[] Settings { get; set; }
        public BaseUserResponseModel[] Friends { get; set; }
        public BaseUserResponseModel[] SubscribesTo { get; set; }
        public BaseUserResponseModel[] Subscribers { get; set; }
        public BaseUserResponseModel[] BlackList { get; set; }
    }
}
