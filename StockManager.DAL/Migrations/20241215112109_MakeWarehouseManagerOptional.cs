using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MakeWarehouseManagerOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "456eb7a9-f83e-4243-8969-8af7f71a9eee");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9536ef3b-2c85-4713-b6b8-1b9ba85c86be");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c13a296-dcf2-4a39-ae55-356befdabde6", null, "admin", "ADMIN" },
                    { "c011700c-40ed-422b-9366-93a57c10478c", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4c13a296-dcf2-4a39-ae55-356befdabde6");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c011700c-40ed-422b-9366-93a57c10478c");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "456eb7a9-f83e-4243-8969-8af7f71a9eee", null, "admin", "ADMIN" },
                    { "9536ef3b-2c85-4713-b6b8-1b9ba85c86be", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }
    }
}
