using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auth.Data.Migrations
{
    public partial class AddedAccountSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "BirthDay", "Email", "ForgotPasswordToken", "IsEmailVerified", "Password", "ProviderId", "SystemRole", "UserName", "VerifyEmailToken" },
                values: new object[] { new Guid("140505f0-9b68-4b74-b33a-51979ddcd8d2"), new DateTime(2019, 8, 5, 21, 23, 31, 83, DateTimeKind.Local).AddTicks(4230), "admin@madjuns.com", null, false, "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", null, 0, "super admin", "344d5814-9b5a-466b-9c44-3889c1be4ab5" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "BirthDay", "Email", "ForgotPasswordToken", "IsEmailVerified", "Password", "ProviderId", "SystemRole", "UserName", "VerifyEmailToken" },
                values: new object[] { new Guid("ecf75ef2-63ee-4e38-86e8-1baaf91789ac"), new DateTime(2019, 8, 5, 21, 23, 31, 86, DateTimeKind.Local).AddTicks(8969), "moderator@madjuns.com", null, false, "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", null, 1, "moderaot", "98179b8e-8b95-4df8-9ec9-92b88518fb89" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("140505f0-9b68-4b74-b33a-51979ddcd8d2"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("ecf75ef2-63ee-4e38-86e8-1baaf91789ac"));
        }
    }
}
