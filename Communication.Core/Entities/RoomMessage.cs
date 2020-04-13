using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class RoomMessage : BaseEntity
    {
        public Guid ChannelMemberId { get; set; }
        public ChannelMember ChannelMember { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public string Message { get; set; }
        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
