using System;

namespace Common.Core.Events
{
    public class UserDeletedEvent
    {
        public Guid AcountId { get; set; }
    }
}
