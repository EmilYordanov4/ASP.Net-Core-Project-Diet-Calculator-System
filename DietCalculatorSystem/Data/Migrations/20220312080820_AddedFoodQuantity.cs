using Microsoft.EntityFrameworkCore.Migrations;

namespace DietCalculatorSystem.Data.Migrations
{
    public partial class AddedFoodQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TotalFoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "LunchFoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "DinnerFoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BreakfastFoods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TotalFoods");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LunchFoods");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DinnerFoods");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BreakfastFoods");
        }
    }
}
