using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        public Guid CreatedByMemberId { get; set; }
        public ChannelMember CreatedByMember { get; set; }

        public ICollection<RoomMessage> Messages { get; set; }
    }
}
