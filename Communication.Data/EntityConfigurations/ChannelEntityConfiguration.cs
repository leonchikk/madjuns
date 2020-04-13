using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class ChannelEntityConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Rooms)
                .WithOne(e => e.Channel)
                .HasForeignKey(e => e.ChannelId);

            builder.HasMany(e => e.ChannelMembers)
                .WithOne(e => e.Channel)
                .HasForeignKey(e => e.ChannelId);
        }
    }
}
