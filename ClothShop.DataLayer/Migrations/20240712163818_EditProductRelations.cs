using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditProductRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_MaterialId",
                table: "ProductMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Materials_MaterialId",
                table: "ProductMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Materials_MaterialId",
                table: "ProductMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterials_MaterialId",
                table: "ProductMaterials");
        }
    }
}
