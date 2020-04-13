using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        public ICollection<PermissionInRole> RolePermissions { get; set; }
        public ICollection<ChannelMemberRole> UsedByMembers { get; set; }
    }
}
