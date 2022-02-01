using Microsoft.EntityFrameworkCore.Migrations;

namespace DietCalculatorSystem.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeficitDiets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalCalories = table.Column<int>(type: "int", nullable: false),
                    TotalProteins = table.Column<int>(type: "int", nullable: false),
                    TotalFats = table.Column<int>(type: "int", nullable: false),
                    TotalCarbohydrates = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BreakfastCalories = table.Column<int>(type: "int", nullable: false),
                    BreakfastProteins = table.Column<int>(type: "int", nullable: false),
                    BreakfastFats = table.Column<int>(type: "int", nullable: false),
                    BreakfastCarbohydrates = table.Column<int>(type: "int", nullable: false),
                    LunchCalories = table.Column<int>(type: "int", nullable: false),
                    LunchProteins = table.Column<int>(type: "int", nullable: false),
                    LunchFats = table.Column<int>(type: "int", nullable: false),
                    LunchCarbohydrates = table.Column<int>(type: "int", nullable: false),
                    DinnerCalories = table.Column<int>(type: "int", nullable: false),
                    DinnerProteins = table.Column<int>(type: "int", nullable: false),
                    DinnerFats = table.Column<int>(type: "int", nullable: false),
                    DinnerCarbohydrates = table.Column<int>(type: "int", nullable: false),
                    SnackCalories = table.Column<int>(type: "int", nullable: false),
                    SnackProteins = table.Column<int>(type: "int", nullable: false),
                    SnackFats = table.Column<int>(type: "int", nullable: false),
                    SnackCarbohydrates = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeficitDiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeficitDiets_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Proteins = table.Column<int>(type: "int", nullable: false),
                    Fats = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates = table.Column<int>(type: "int", nullable: false),
                    DeficitDietId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeficitDietId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeficitDietId2 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeficitDietId3 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_DeficitDiets_DeficitDietId",
                        column: x => x.DeficitDietId,
                        principalTable: "DeficitDiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foods_DeficitDiets_DeficitDietId1",
                        column: x => x.DeficitDietId1,
                        principalTable: "DeficitDiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foods_DeficitDiets_DeficitDietId2",
                        column: x => x.DeficitDietId2,
                        principalTable: "DeficitDiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foods_DeficitDiets_DeficitDietId3",
                        column: x => x.DeficitDietId3,
                        principalTable: "DeficitDiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeficitDiets_UserId1",
                table: "DeficitDiets",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DeficitDietId",
                table: "Foods",
                column: "DeficitDietId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DeficitDietId1",
                table: "Foods",
                column: "DeficitDietId1");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DeficitDietId2",
                table: "Foods",
                column: "DeficitDietId2");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DeficitDietId3",
                table: "Foods",
                column: "DeficitDietId3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "DeficitDiets");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
