using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class AddedUserNameAndRemovedCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId2",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserId2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Profiles",
                newName: "UserName");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Addresses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Profiles",
                newName: "LastName");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId2",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Profiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId1",
                table: "Users",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId2",
                table: "Users",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId1",
                table: "Users",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId2",
                table: "Users",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
