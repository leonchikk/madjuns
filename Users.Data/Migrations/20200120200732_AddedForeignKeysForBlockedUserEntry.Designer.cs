﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Data;

namespace Users.Data.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20200120200732_AddedForeignKeysForBlockedUserEntry")]
    partial class AddedForeignKeysForBlockedUserEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Users.Core.Domain.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Users.Core.Domain.BlockedUser", b =>
                {
                    b.Property<Guid>("InitiatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WhoisBlockedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("InitiatorId", "WhoisBlockedId");

                    b.HasIndex("WhoisBlockedId");

                    b.ToTable("UserBlackList");
                });

            modelBuilder.Entity("Users.Core.Domain.FriendsShip", b =>
                {
                    b.Property<Guid>("IAmId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MyFriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("IAmId", "MyFriendId");

                    b.HasIndex("MyFriendId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Users.Core.Domain.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DayOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Users.Core.Domain.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("SettingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Users.Core.Domain.UserSubscriber", b =>
                {
                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("SubscriberId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscribers");
                });

            modelBuilder.Entity("Users.Core.Domain.BlockedUser", b =>
                {
                    b.HasOne("Users.Core.Domain.User", "Initiator")
                        .WithMany("UsersBlockedByMe")
                        .HasForeignKey("InitiatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Users.Core.Domain.User", "WhoisBlocked")
                        .WithMany("IAmBlockedByUsers")
                        .HasForeignKey("WhoisBlockedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Users.Core.Domain.FriendsShip", b =>
                {
                    b.HasOne("Users.Core.Domain.User", "IAm")
                        .WithMany("IAmFriendsWith")
                        .HasForeignKey("IAmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Users.Core.Domain.User", "MyFriend")
                        .WithMany("AreFriendsWithMe")
                        .HasForeignKey("MyFriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Users.Core.Domain.User", "User")
                        .WithMany("Subscribers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
