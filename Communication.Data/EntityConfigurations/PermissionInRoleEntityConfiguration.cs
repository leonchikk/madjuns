using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class PermissionInRoleEntityConfiguration : IEntityTypeConfiguration<ChannelMemberRole>
    {
        public void Configure(EntityTypeBuilder<ChannelMemberRole> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ChannelMember);
            builder.HasOne(e => e.Role);
        }
    }
}
