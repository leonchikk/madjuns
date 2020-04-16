using Microsoft.EntityFrameworkCore.Migrations;

namespace Communication.Data.Migrations
{
    public partial class AddLogoUrlAndVisibilityToChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "Channels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Channels");
        }
    }
}
