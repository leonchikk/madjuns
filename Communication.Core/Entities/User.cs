using Common.Core.Models;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public ICollection<ChannelMember> ConsistsInChannels { get; set; }
        public ICollection<UserMessage> ToMessages { get; set; }
        public ICollection<UserMessage> FromMessages { get; set; }
    }
}
