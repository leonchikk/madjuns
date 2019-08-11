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
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Friends);
                entity.HasMany(e => e.Subscribers);
                entity.HasMany(e => e.BlackList);
                entity.HasOne(e => e.Profile);
                entity.HasMany(e => e.Settings);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Address);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasData(new Setting() { Id = new Guid("44c06109-dba4-4723-a38b-225a88ac8fac"), Name = "Private account" });
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Setting);
                entity.HasOne(e => e.User);
            });
        }
    }
}
