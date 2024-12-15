using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductWarehouseStockQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsProductId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehousesWarehouseId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouse_WarehousesWarehouseId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "55fd1897-53c6-4ec5-9365-25e2591a0cbb");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6714912a-0764-42fa-9766-6b1cc07e58b6");

            migrationBuilder.RenameColumn(
                name: "WarehousesWarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                newName: "StockQuantity");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                schema: "dbo",
                table: "ProductWarehouse",
                newName: "WarehouseId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "dbo",
                table: "ProductWarehouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                schema: "dbo",
                table: "ProductWarehouse",
                columns: new[] { "ProductId", "WarehouseId" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6d03cd81-7251-4123-9366-db4019561b5c", null, "warehouse manager", "WAREHOUSE MANAGER" },
                    { "a6b998c3-94e9-403b-9750-9f20793eefce", null, "admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Products_ProductId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "WarehouseId",
                principalSchema: "dbo",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehouseId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                schema: "dbo",
                table: "ProductWarehouse");

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

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "dbo",
                table: "ProductWarehouse");

            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                schema: "dbo",
                table: "ProductWarehouse",
                newName: "WarehousesWarehouseId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                newName: "ProductsProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                schema: "dbo",
                table: "ProductWarehouse",
                columns: new[] { "ProductsProductId", "WarehousesWarehouseId" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55fd1897-53c6-4ec5-9365-25e2591a0cbb", null, "admin", "ADMIN" },
                    { "6714912a-0764-42fa-9766-6b1cc07e58b6", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_WarehousesWarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "WarehousesWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsProductId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "ProductsProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Warehouses_WarehousesWarehouseId",
                schema: "dbo",
                table: "ProductWarehouse",
                column: "WarehousesWarehouseId",
                principalSchema: "dbo",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
