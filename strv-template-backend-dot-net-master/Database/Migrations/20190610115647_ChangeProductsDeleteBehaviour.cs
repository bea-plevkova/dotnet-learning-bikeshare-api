using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ChangeProductsDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_products_product_categories_product_category_id",
                "products");

            migrationBuilder.AddForeignKey(
                "FK_products_product_categories_product_category_id",
                "products",
                "product_category_id",
                "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_products_product_categories_product_category_id",
                "products");

            migrationBuilder.AddForeignKey(
                "FK_products_product_categories_product_category_id",
                "products",
                "product_category_id",
                "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}