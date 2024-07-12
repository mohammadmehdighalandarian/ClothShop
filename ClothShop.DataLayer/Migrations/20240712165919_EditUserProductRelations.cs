using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditUserProductRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUserProduct");

            migrationBuilder.DropTable(
                name: "UserUserProduct");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_ProductId",
                table: "UserProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_UserId",
                table: "UserProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Products_ProductId",
                table: "UserProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Users_UserId",
                table: "UserProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Products_ProductId",
                table: "UserProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Users_UserId",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserProducts_ProductId",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserProducts_UserId",
                table: "UserProducts");

            migrationBuilder.CreateTable(
                name: "ProductUserProduct",
                columns: table => new
                {
                    ProductsProductId = table.Column<int>(type: "int", nullable: false),
                    UserProductsUserProduct_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserProduct", x => new { x.ProductsProductId, x.UserProductsUserProduct_Id });
                    table.ForeignKey(
                        name: "FK_ProductUserProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUserProduct_UserProducts_UserProductsUserProduct_Id",
                        column: x => x.UserProductsUserProduct_Id,
                        principalTable: "UserProducts",
                        principalColumn: "UserProduct_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserUserProduct",
                columns: table => new
                {
                    UserProductsUserProduct_Id = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserProduct", x => new { x.UserProductsUserProduct_Id, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserUserProduct_UserProducts_UserProductsUserProduct_Id",
                        column: x => x.UserProductsUserProduct_Id,
                        principalTable: "UserProducts",
                        principalColumn: "UserProduct_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserUserProduct_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserProduct_UserProductsUserProduct_Id",
                table: "ProductUserProduct",
                column: "UserProductsUserProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserUserProduct_UsersUserId",
                table: "UserUserProduct",
                column: "UsersUserId");
        }
    }
}
