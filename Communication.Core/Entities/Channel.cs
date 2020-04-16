using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Communication.Core.Entities
{
    public class Channel : BaseEntity
    {
        public Channel()
        {
            Id = Guid.NewGuid();
            Rooms = new HashSet<Room>();
            ChannelMembers = new HashSet<ChannelMember>();
            ChannelRoles = new HashSet<Role>();
        }

        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ChannelVisibility Visibility { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<ChannelMember> ChannelMembers { get; set; }
        public ICollection<Role> ChannelRoles { get; set; }
    }

    public enum ChannelVisibility
    {
        Public,
        Private
    }
}
