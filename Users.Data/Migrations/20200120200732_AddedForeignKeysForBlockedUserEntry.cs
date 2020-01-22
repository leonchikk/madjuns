using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Users.Data.Migrations
{
    public partial class AddedForeignKeysForBlockedUserEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlackList",
                table: "UserBlackList");

            migrationBuilder.DropIndex(
                name: "IX_UserBlackList_InitiatorId",
                table: "UserBlackList");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhoisBlockedId",
                table: "UserBlackList",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InitiatorId",
                table: "UserBlackList",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlackList",
                table: "UserBlackList",
                columns: new[] { "InitiatorId", "WhoisBlockedId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlackList",
                table: "UserBlackList");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhoisBlockedId",
                table: "UserBlackList",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "InitiatorId",
                table: "UserBlackList",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlackList",
                table: "UserBlackList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlackList_InitiatorId",
                table: "UserBlackList",
                column: "InitiatorId");
        }
    }
}
