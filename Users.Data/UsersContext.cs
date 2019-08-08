using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Core.Domain;

namespace Users.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext()
        {

        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Friends);
                entity.HasMany(e => e.Subscribers);
                entity.HasMany(e => e.BlackList);
                entity.HasOne(e => e.Profile);
                entity.HasOne(e => e.Settings);
            });
        }
    }
}
