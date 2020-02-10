using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class UserMessage : BaseEntity
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }

        public User FromUser { get; set; }
        public User ToUser { get; set; }
        
        public string Message { get; set; }
        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
