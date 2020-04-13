using Communication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Communication.Data.EntityConfigurations
{
    public class UserMessageEntityConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.FromUser);
            builder.HasOne(e => e.ToUser);
        }
    }
}
