using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class BlockedUser : BaseEntity
    {
        protected BlockedUser() { }
        public BlockedUser(Guid initiatorId, Guid whoIsBannedId)
        {
            InitiatorId = initiatorId;
            WhoisBlockedId = whoIsBannedId;
        }

        public virtual Guid InitiatorId { get; set; }
        public virtual Guid WhoisBlockedId { get; set; }

        public virtual User Initiator { get; set; }
        public virtual User WhoisBlocked { get; set; }
    }
}
