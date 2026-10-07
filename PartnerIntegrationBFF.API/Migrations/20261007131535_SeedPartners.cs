using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartnerIntegrationBFF.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedPartners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "IsActive", "Name", "PartnerCode" },
                values: new object[,]
                {
                    { 1, true, "AcmeCorp", "P-1001" },
                    { 2, true, "Globex", "P-1002" },
                    { 3, false, "Umbrella", "P-1003" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
