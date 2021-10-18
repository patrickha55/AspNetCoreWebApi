using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Data.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd8c984c-2c2e-4a8a-b964-32ee3642382e", "18ec74e7-fad7-4bc4-975c-49de97e97f7d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16ee2838-79a4-4e16-be22-cf5cf05accf0", "c48513d2-eea5-4fc7-af77-2912d369edd5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16ee2838-79a4-4e16-be22-cf5cf05accf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd8c984c-2c2e-4a8a-b964-32ee3642382e");
        }
    }
}
