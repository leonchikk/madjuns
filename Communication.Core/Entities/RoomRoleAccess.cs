using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class RoomRoleAccess : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid RoomId { get; set; }

        public Room Room { get; set; }
        public Role Role { get; set; }
    }
}
