using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class ChannelMemberEntityConfiguration : IEntityTypeConfiguration<ChannelMember>
    {
        public void Configure(EntityTypeBuilder<ChannelMember> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Channel)
                .WithMany(e => e.ChannelMembers)
                .HasForeignKey(e => e.ChannelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
                .WithMany(e => e.ConsistsInChannels)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
