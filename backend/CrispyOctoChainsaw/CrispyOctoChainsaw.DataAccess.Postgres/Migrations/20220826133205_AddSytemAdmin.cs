using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrispyOctoChainsaw.DataAccess.Postgres.Migrations
{
    public partial class AddSytemAdmin : Migration
    {
        private const string ADMIN_ID = "51debc66-d7e2-4e42-bcbc-5f8fcbb168a1";
        private const string ROLE_ID = "d3befd2b-b22c-4c38-91af-2a6d5baf7446";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var passwordHash = hasher.HashPassword(null, "Password100!");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "UserName",
                    "PasswordHash",
                    "EmailConfirmed",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount"
                },
                values: new object[]
                {
                    ADMIN_ID,
                    "SystemAdmin",
                    passwordHash,
                    false,
                    false,
                    false,
                    false,
                    0
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[]
                {
                    "Id",
                    "Name"
                },
                values: new object[]
                {
                    ROLE_ID,
                    "SystemAdmin"
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[]
                {
                    "UserId",
                    "RoleId"
                },
                values: new object[]
                {
                    ADMIN_ID,
                    ROLE_ID
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: ROLE_ID);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: ADMIN_ID);
        }
    }
}
