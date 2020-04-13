using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.ConsistsInChannels)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.ToMessages)
                .WithOne(e => e.ToUser)
                .HasForeignKey(e => e.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.FromMessages)
                .WithOne(e => e.FromUser)
                .HasForeignKey(e => e.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
