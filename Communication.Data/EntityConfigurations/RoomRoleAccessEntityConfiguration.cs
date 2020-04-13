using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class RoomRoleAccessEntityConfiguration : IEntityTypeConfiguration<RoomRoleAccess>
    {
        public void Configure(EntityTypeBuilder<RoomRoleAccess> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Room)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
