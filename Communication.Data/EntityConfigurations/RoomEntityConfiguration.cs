using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Messages)
                .WithOne(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            builder.HasOne(e => e.Channel)
                .WithMany(e => e.Rooms)
                .HasForeignKey(e => e.ChannelId);

            builder.HasOne(e => e.CreatedByMember)
                .WithMany(e => e.OwnedRooms)
                .HasForeignKey(e => e.CreatedByMemberId);
        }
    }
}
