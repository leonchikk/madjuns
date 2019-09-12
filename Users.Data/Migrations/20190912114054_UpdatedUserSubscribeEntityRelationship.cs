using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class UpdatedUserSubscribeEntityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscribers_Users_UserId1",
                table: "UserSubscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscribers",
                table: "UserSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscribers_SubscriberId",
                table: "UserSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscribers_UserId1",
                table: "UserSubscribers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserSubscribers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserSubscribers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "UserSubscribers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscribers",
                table: "UserSubscribers",
                columns: new[] { "SubscriberId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscribers",
                table: "UserSubscribers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserSubscribers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "UserSubscribers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserSubscribers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscribers",
                table: "UserSubscribers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_SubscriberId",
                table: "UserSubscribers",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscribers_UserId1",
                table: "UserSubscribers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscribers_Users_UserId1",
                table: "UserSubscribers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
