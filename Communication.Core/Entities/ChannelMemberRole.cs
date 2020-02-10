using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class ChannelMemberRole : BaseEntity
    {
        public Guid ChannelMemberId { get; set; }
        public Guid RoleId { get; set; }

        public ChannelMember ChannelMember { get; set; }
        public Role Role { get; set; }
    }
}
