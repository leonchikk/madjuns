using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Data.Migrations
{
    public partial class AddedForgotPasswordToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordToken",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgotPasswordToken",
                table: "Accounts");
        }
    }
}
