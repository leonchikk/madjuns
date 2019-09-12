using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class UpdatedPropertiesNamesInUserFriendEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_FirstUserId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_SecondUserId",
                table: "UserFriends");

            migrationBuilder.RenameColumn(
                name: "SecondUserId",
                table: "UserFriends",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "FirstUserId",
                table: "UserFriends",
                newName: "FriendId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_SecondUserId",
                table: "UserFriends",
                newName: "IX_UserFriends_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_FirstUserId",
                table: "UserFriends",
                newName: "IX_UserFriends_FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_UserId1",
                table: "UserFriends",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_FriendId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_UserId1",
                table: "UserFriends");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "UserFriends",
                newName: "SecondUserId");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "UserFriends",
                newName: "FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_UserId1",
                table: "UserFriends",
                newName: "IX_UserFriends_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                newName: "IX_UserFriends_FirstUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_FirstUserId",
                table: "UserFriends",
                column: "FirstUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_SecondUserId",
                table: "UserFriends",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
