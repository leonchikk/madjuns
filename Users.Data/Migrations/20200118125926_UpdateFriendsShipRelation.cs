using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class UpdateFriendsShipRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlackList_Users_BannedUserId",
                table: "UserBlackList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlackList_Users_UserId",
                table: "UserBlackList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_FriendId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_BannedUserId",
                table: "UserBlackList");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_UserId",
                table: "UserBlackList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "BannedUserId",
                table: "UserBlackList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBlackList");

            migrationBuilder.AddColumn<Guid>(
                name: "IAmId",
                table: "UserFriends",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MyFriendId",
                table: "UserFriends",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InitiatorId",
                table: "UserBlackList",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhoisBlockedId",
                table: "UserBlackList",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                columns: new[] { "IAmId", "MyFriendId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_MyFriendId",
                table: "UserFriends",
                column: "MyFriendId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_InitiatorId",
                table: "UserBlackList",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_WhoisBlockedId",
                table: "UserBlackList",
                column: "WhoisBlockedId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlackList_Users_InitiatorId",
                table: "UserBlackList",
                column: "InitiatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlackList_Users_WhoisBlockedId",
                table: "UserBlackList",
                column: "WhoisBlockedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_IAmId",
                table: "UserFriends",
                column: "IAmId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_MyFriendId",
                table: "UserFriends",
                column: "MyFriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlackList_Users_InitiatorId",
                table: "UserBlackList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlackList_Users_WhoisBlockedId",
                table: "UserBlackList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_IAmId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_MyFriendId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_MyFriendId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_InitiatorId",
                table: "UserBlackList");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_WhoisBlockedId",
                table: "UserBlackList");

            migrationBuilder.DropColumn(
                name: "IAmId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "MyFriendId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "UserBlackList");

            migrationBuilder.DropColumn(
                name: "WhoisBlockedId",
                table: "UserBlackList");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FriendId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BannedUserId",
                table: "UserBlackList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserBlackList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                columns: new[] { "UserId", "FriendId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_BannedUserId",
                table: "UserBlackList",
                column: "BannedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_UserId",
                table: "UserBlackList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlackList_Users_BannedUserId",
                table: "UserBlackList",
                column: "BannedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlackList_Users_UserId",
                table: "UserBlackList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
