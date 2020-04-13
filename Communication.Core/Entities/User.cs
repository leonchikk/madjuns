using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class User : BaseEntity
    {
        public User(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }


        public string UserName { get; set; }
        public ICollection<ChannelMember> ConsistsInChannels { get; set; }
        public ICollection<UserMessage> ToMessages { get; set; }
        public ICollection<UserMessage> FromMessages { get; set; }
    }
}
