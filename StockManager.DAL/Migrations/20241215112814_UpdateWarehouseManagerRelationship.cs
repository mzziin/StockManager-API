using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouseManagerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_WarehouseManagerId",
                schema: "dbo",
                table: "Warehouses");

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
                    { "47fb07bc-e88e-4c93-94e0-a52f3b10388a", null, "admin", "ADMIN" },
                    { "5065dad5-e15b-4baa-9dae-6d7535e1ee49", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_WarehouseManagerId",
                schema: "dbo",
                table: "Warehouses",
                column: "WarehouseManagerId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_WarehouseManagerId",
                schema: "dbo",
                table: "Warehouses");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "47fb07bc-e88e-4c93-94e0-a52f3b10388a");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5065dad5-e15b-4baa-9dae-6d7535e1ee49");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c13a296-dcf2-4a39-ae55-356befdabde6", null, "admin", "ADMIN" },
                    { "c011700c-40ed-422b-9366-93a57c10478c", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_WarehouseManagerId",
                schema: "dbo",
                table: "Warehouses",
                column: "WarehouseManagerId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
