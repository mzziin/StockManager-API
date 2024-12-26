using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPurchaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "890fe5e6-fbfa-45db-a895-1d1f78986842");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "efe52176-8051-4869-bb1a-39f55376b5f0");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "84ea9d48-cd66-4f9b-acdc-9ef680f135aa", null, "admin", "ADMIN" },
                    { "dd513648-319d-4f4a-a7ae-75745023d683", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "84ea9d48-cd66-4f9b-acdc-9ef680f135aa");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dd513648-319d-4f4a-a7ae-75745023d683");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "890fe5e6-fbfa-45db-a895-1d1f78986842", null, "admin", "ADMIN" },
                    { "efe52176-8051-4869-bb1a-39f55376b5f0", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }
    }
}
