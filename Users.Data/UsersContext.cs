using Microsoft.EntityFrameworkCore;
using System;
using Users.Core.Domain;

namespace Users.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<FriendsShip> UserFriends { get; set; }
        public virtual DbSet<UserSubscriber> UserSubscribers { get; set; }
        public virtual DbSet<BlockedUser> UserBlackList { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
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

            modelBuilder.Entity<FriendsShip>(entity =>
            {
                entity.HasKey(e => new { e.IAmId, e.MyFriendId});

                entity.HasOne(e => e.IAm)
                    .WithMany(e => e.IAmFriendsWith)
                    .HasForeignKey(e => e.IAmId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MyFriend)
                    .WithMany(e => e.AreFriendsWithMe)
                    .HasForeignKey(e => e.MyFriendId)
                    .OnDelete(DeleteBehavior.Restrict);
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
                entity.HasKey(e => new { e.InitiatorId, e.WhoisBlockedId });

                entity.HasOne(e => e.Initiator)
                    .WithMany(e => e.UsersBlockedByMe)
                    .HasForeignKey(e => e.InitiatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.WhoisBlocked)
                    .WithMany(e => e.IAmBlockedByUsers)
                    .HasForeignKey(e => e.WhoisBlockedId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
