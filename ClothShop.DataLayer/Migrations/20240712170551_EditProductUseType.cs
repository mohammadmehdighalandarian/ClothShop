using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditProductUseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductUseTypes_ProductId",
                table: "ProductUseTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUseTypes_UseTypeId",
                table: "ProductUseTypes",
                column: "UseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUseTypes_Products_ProductId",
                table: "ProductUseTypes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUseTypes_UseTypes_UseTypeId",
                table: "ProductUseTypes",
                column: "UseTypeId",
                principalTable: "UseTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUseTypes_Products_ProductId",
                table: "ProductUseTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUseTypes_UseTypes_UseTypeId",
                table: "ProductUseTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductUseTypes_ProductId",
                table: "ProductUseTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductUseTypes_UseTypeId",
                table: "ProductUseTypes");
        }
    }
}
