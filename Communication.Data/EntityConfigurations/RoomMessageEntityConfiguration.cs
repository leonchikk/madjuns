using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class RoomMessageEntityConfiguration : IEntityTypeConfiguration<RoomMessage>
    {
        public void Configure(EntityTypeBuilder<RoomMessage> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ChannelMember)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Room)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
