using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Data.Migrations
{
    public partial class AddedVerifyEmailToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerifyEmailToken",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyEmailToken",
                table: "Accounts");
        }
    }
}
