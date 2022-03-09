using Microsoft.EntityFrameworkCore.Migrations;

namespace DietCalculatorSystem.Data.Migrations
{
    public partial class AddedIEnumerablesIntoDiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DietId",
                table: "Foods",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DietId1",
                table: "Foods",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DietId2",
                table: "Foods",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DietId",
                table: "Foods",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DietId1",
                table: "Foods",
                column: "DietId1");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DietId2",
                table: "Foods",
                column: "DietId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Diets_DietId",
                table: "Foods",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Diets_DietId1",
                table: "Foods",
                column: "DietId1",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Diets_DietId2",
                table: "Foods",
                column: "DietId2",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Diets_DietId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Diets_DietId1",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Diets_DietId2",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_DietId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_DietId1",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_DietId2",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "DietId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "DietId1",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "DietId2",
                table: "Foods");
        }
    }
}
