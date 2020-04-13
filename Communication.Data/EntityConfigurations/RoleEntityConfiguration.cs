using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Channel)
                .WithMany(e => e.ChannelRoles)
                .HasForeignKey(e => e.ChannelId);

            builder.HasMany(e => e.RolePermissions)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
