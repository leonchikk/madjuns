using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext()
        {

        }

        public AuthContext(DbContextOptions<AuthContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
