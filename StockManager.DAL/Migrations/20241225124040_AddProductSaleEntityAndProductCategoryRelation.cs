using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSaleEntityAndProductCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSale_Products_ProductsProductId",
                schema: "dbo",
                table: "ProductSale");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSale_Sales_SalesSaleId",
                schema: "dbo",
                table: "ProductSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSale",
                schema: "dbo",
                table: "ProductSale");

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

            migrationBuilder.RenameColumn(
                name: "SalesSaleId",
                schema: "dbo",
                table: "ProductSale",
                newName: "SaleId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                schema: "dbo",
                table: "ProductSale",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSale_SalesSaleId",
                schema: "dbo",
                table: "ProductSale",
                newName: "IX_ProductSale_SaleId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "dbo",
                table: "ProductSale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSale",
                schema: "dbo",
                table: "ProductSale",
                columns: new[] { "ProductId", "SaleId" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "890fe5e6-fbfa-45db-a895-1d1f78986842", null, "admin", "ADMIN" },
                    { "efe52176-8051-4869-bb1a-39f55376b5f0", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSale_Products_ProductId",
                schema: "dbo",
                table: "ProductSale",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSale_Sales_SaleId",
                schema: "dbo",
                table: "ProductSale",
                column: "SaleId",
                principalSchema: "dbo",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSale_Products_ProductId",
                schema: "dbo",
                table: "ProductSale");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSale_Sales_SaleId",
                schema: "dbo",
                table: "ProductSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSale",
                schema: "dbo",
                table: "ProductSale");

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

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "dbo",
                table: "ProductSale");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "dbo",
                table: "ProductSale",
                newName: "ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                schema: "dbo",
                table: "ProductSale",
                newName: "SalesSaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSale_SaleId",
                schema: "dbo",
                table: "ProductSale",
                newName: "IX_ProductSale_SalesSaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSale",
                schema: "dbo",
                table: "ProductSale",
                columns: new[] { "ProductsProductId", "SalesSaleId" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b5a5aef-0592-4d80-865d-40d36712d136", null, "admin", "ADMIN" },
                    { "97c9d27c-665b-4dfb-a2be-5b0086d6ce78", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSale_Products_ProductsProductId",
                schema: "dbo",
                table: "ProductSale",
                column: "ProductsProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSale_Sales_SalesSaleId",
                schema: "dbo",
                table: "ProductSale",
                column: "SalesSaleId",
                principalSchema: "dbo",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
