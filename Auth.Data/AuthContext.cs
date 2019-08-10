using Auth.Core.Entities;
using Auth.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;

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

            modelBuilder.Entity<Account>().HasData(new Account("admin@madjuns.com", "123456", "super admin", DateTime.Now, SystemRoles.Admin));
            modelBuilder.Entity<Account>().HasData(new Account("moderator@madjuns.com", "123456", "moderaot", DateTime.Now, SystemRoles.Moderator));
        }
    }
}
