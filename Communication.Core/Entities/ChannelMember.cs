using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class ChannelMember : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }

        public User User { get; set; }
        public Channel Channel { get; set; }

        public ICollection<Room> OwnedRooms { get; set; }
        public ICollection<ChannelMemberRole> Roles { get; set; }
    }
}
