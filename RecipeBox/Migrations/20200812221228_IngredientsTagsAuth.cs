using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBox.Migrations
{
    public partial class IngredientsTagsAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_AspNetUsers_UserId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UserId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ingredients");
        }
    }
}
