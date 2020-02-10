using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class RoomMessage : BaseEntity
    {
        public Guid FromChannelMemberId { get; set; }
        public ChannelMember ChannelMember { get; set; }

        public string Message { get; set; }
        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
