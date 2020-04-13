using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class ChannelMemberRoleEntityConfiguration : IEntityTypeConfiguration<ChannelMemberRole>
    {
        public void Configure(EntityTypeBuilder<ChannelMemberRole> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ChannelMember)
                .WithMany(e => e.Roles)
                .HasForeignKey(e => e.ChannelMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Role)
                .WithMany(e => e.UsedByMembers)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
