using Communication.Core.Entities;
using Communication.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Communication.Data
{
    public class CommunicationsContext : DbContext
    {
        public CommunicationsContext(DbContextOptions<CommunicationsContext> options)
           : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelMember> ChannelMembers { get; set; }
        public virtual DbSet<ChannelMemberRole> ChannelMembersRoles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionInRole> PermissionsInRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomMessage> RoomsMessages { get; set; }
        public virtual DbSet<RoomRoleAccess> RoomRolesAccesses { get; set; }
        public virtual DbSet<UserMessage> UsersMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelMemberEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelMemberRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionInRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoomMessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoomRoleAccessEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserMessageEntityConfiguration());
        }
    }
}
