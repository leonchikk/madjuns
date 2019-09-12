using Microsoft.EntityFrameworkCore;
using System;
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
        public virtual DbSet<UserFriend> UserFriends { get; set; }
        public virtual DbSet<UserSubscriber> UserSubscribers { get; set; }
        public virtual DbSet<BlockedUser> UserBlackList { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Profile);
                entity.HasMany(e => e.Settings);
                entity.HasMany(e => e.Friends);
                entity.HasMany(e => e.BlackList);
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

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User);
                entity.HasOne(e => e.Friend);
            });

            modelBuilder.Entity<UserSubscriber>(entity =>
            {
                entity.HasKey(e => new { e.SubscriberId, e.UserId });

                entity.HasOne(e => e.Subscriber)
                    .WithMany(e => e.SubscribesTo)
                    .HasForeignKey(e => e.SubscriberId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Subscribers)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BlockedUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User);
                entity.HasOne(e => e.BannedUser);
            });
        }
    }
}
