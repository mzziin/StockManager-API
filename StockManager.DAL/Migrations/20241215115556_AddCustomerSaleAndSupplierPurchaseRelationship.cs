using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerSaleAndSupplierPurchaseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "CustomerName",
                schema: "dbo",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "dbo",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                schema: "dbo",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "IX_Sales_CustomerId",
                schema: "dbo",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                schema: "dbo",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                schema: "dbo",
                table: "Purchases",
                column: "SupplierId",
                principalSchema: "dbo",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                schema: "dbo",
                table: "Sales",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                schema: "dbo",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CustomerId",
                schema: "dbo",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SupplierId",
                schema: "dbo",
                table: "Purchases");

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

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                schema: "dbo",
                table: "Sales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                schema: "dbo",
                table: "Purchases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47fb07bc-e88e-4c93-94e0-a52f3b10388a", null, "admin", "ADMIN" },
                    { "5065dad5-e15b-4baa-9dae-6d7535e1ee49", null, "warehouse manager", "WAREHOUSE MANAGER" }
                });
        }
    }
}
