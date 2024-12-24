using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MakeSubcategoryIdNullableInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6d03cd81-7251-4123-9366-db4019561b5c");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a6b998c3-94e9-403b-9750-9f20793eefce");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                schema: "dbo",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b5a5aef-0592-4d80-865d-40d36712d136", null, "admin", "ADMIN" },
                    { "97c9d27c-665b-4dfb-a2be-5b0086d6ce78", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5b5a5aef-0592-4d80-865d-40d36712d136");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "97c9d27c-665b-4dfb-a2be-5b0086d6ce78");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                schema: "dbo",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6d03cd81-7251-4123-9366-db4019561b5c", null, "warehouse manager", "WAREHOUSE MANAGER" },
                    { "a6b998c3-94e9-403b-9750-9f20793eefce", null, "admin", "ADMIN" }
                });
        }
    }
}
