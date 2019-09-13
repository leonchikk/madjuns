using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class UpdatedBlockedUserEntityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlackList_Users_UserId1",
                table: "UserBlackList");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_UserId1",
                table: "UserBlackList");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserBlackList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserBlackList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_UserId1",
                table: "UserBlackList",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlackList_Users_UserId1",
                table: "UserBlackList",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
