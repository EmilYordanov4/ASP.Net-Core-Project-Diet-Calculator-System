using Microsoft.EntityFrameworkCore.Migrations;

namespace DietCalculatorSystem.Data.Migrations
{
    public partial class AddedIEnumerablesIntoDietTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakfastFoods");

            migrationBuilder.DropTable(
                name: "DinnerFoods");

            migrationBuilder.DropTable(
                name: "LunchFoods");

            migrationBuilder.DropTable(
                name: "TotalFoods");

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

            migrationBuilder.AddColumn<string>(
                name: "DietId3",
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

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DietId3",
                table: "Foods",
                column: "DietId3");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Diets_DietId3",
                table: "Foods",
                column: "DietId3",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Diets_DietId3",
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

            migrationBuilder.DropIndex(
                name: "IX_Foods_DietId3",
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

            migrationBuilder.DropColumn(
                name: "DietId3",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "BreakfastFoods",
                columns: table => new
                {
                    DietId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakfastFoods", x => new { x.DietId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_BreakfastFoods_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BreakfastFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DinnerFoods",
                columns: table => new
                {
                    DietId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerFoods", x => new { x.DietId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_DinnerFoods_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DinnerFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LunchFoods",
                columns: table => new
                {
                    DietId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchFoods", x => new { x.DietId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_LunchFoods_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalFoods",
                columns: table => new
                {
                    DietId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalFoods", x => new { x.DietId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_TotalFoods_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TotalFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakfastFoods_FoodId",
                table: "BreakfastFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerFoods_FoodId",
                table: "DinnerFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchFoods_FoodId",
                table: "LunchFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_TotalFoods_FoodId",
                table: "TotalFoods",
                column: "FoodId");
        }
    }
}
