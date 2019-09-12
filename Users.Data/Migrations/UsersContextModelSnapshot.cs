﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Data;

namespace Users.Data.Migrations
{
    [DbContext(typeof(UsersContext))]
    partial class UsersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Users.Core.Domain.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("District");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Users.Core.Domain.BlockedUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BannedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("UserId");

                    b.Property<Guid?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("BannedUserId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserBlackList");
                });

            modelBuilder.Entity("Users.Core.Domain.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<DateTime>("DayOfBirth");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Users.Core.Domain.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("44c06109-dba4-4723-a38b-225a88ac8fac"),
                            IsDeleted = false,
                            Name = "Private account"
                        });
                });

            modelBuilder.Entity("Users.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Users.Core.Domain.UserFriend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FriendId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("UserId");

                    b.Property<Guid?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("SettingId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSubscriber", b =>
                {
                    b.Property<Guid>("SubscriberId");

                    b.Property<Guid>("UserId");

                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("SubscriberId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscribers");
                });

            modelBuilder.Entity("Users.Core.Domain.BlockedUser", b =>
                {
                    b.HasOne("Users.Core.Domain.User", "BannedUser")
                        .WithMany()
                        .HasForeignKey("BannedUserId");

                    b.HasOne("Users.Core.Domain.User")
                        .WithMany("BlackList")
                        .HasForeignKey("UserId");

                    b.HasOne("Users.Core.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Users.Core.Domain.Profile", b =>
                {
                    b.HasOne("Users.Core.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Users.Core.Domain.User", b =>
                {
                    b.HasOne("Users.Core.Domain.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("Users.Core.Domain.UserFriend", b =>
                {
                    b.HasOne("Users.Core.Domain.User", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId");

                    b.HasOne("Users.Core.Domain.User")
                        .WithMany("Friends")
                        .HasForeignKey("UserId");

                    b.HasOne("Users.Core.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSetting", b =>
                {
                    b.HasOne("Users.Core.Domain.Setting", "Setting")
                        .WithMany()
                        .HasForeignKey("SettingId");

                    b.HasOne("Users.Core.Domain.User", "User")
                        .WithMany("Settings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSubscriber", b =>
                {
                    b.HasOne("Users.Core.Domain.User", "Subscriber")
                        .WithMany("SubscribesTo")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Users.Core.Domain.User", "User")
                        .WithMany("Subscribers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
