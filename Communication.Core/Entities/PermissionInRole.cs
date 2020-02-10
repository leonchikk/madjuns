using Common.Core.Models;
using System;

namespace Communication.Core.Entities
{
    public class PermissionInRole : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
