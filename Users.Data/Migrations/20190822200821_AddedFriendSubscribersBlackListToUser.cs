using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Users.Data.Migrations
{
    public partial class AddedFriendSubscribersBlackListToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBlackList",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId1 = table.Column<Guid>(nullable: true),
                    BannedUserId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlackList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBlackList_Users_BannedUserId",
                        column: x => x.BannedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBlackList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBlackList_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFriends",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstUserId = table.Column<Guid>(nullable: true),
                    SecondUserId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_FirstUserId",
                        column: x => x.FirstUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId1 = table.Column<Guid>(nullable: true),
                    SubscriberId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscribers_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscribers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscribers_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_BannedUserId",
                table: "UserBlackList",
                column: "BannedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_UserId",
                table: "UserBlackList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_UserId1",
                table: "UserBlackList",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FirstUserId",
                table: "UserFriends",
                column: "FirstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_SecondUserId",
                table: "UserFriends",
                column: "SecondUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_SubscriberId",
                table: "UserSubscribers",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_UserId",
                table: "UserSubscribers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_UserId1",
                table: "UserSubscribers",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBlackList");

            migrationBuilder.DropTable(
                name: "UserFriends");

            migrationBuilder.DropTable(
                name: "UserSubscribers");
        }
    }
}
