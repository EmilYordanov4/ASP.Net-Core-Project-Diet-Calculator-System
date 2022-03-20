using Microsoft.EntityFrameworkCore.Migrations;

namespace DietCalculatorSystem.Data.Migrations
{
    public partial class AddedPublicOptionToFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Foods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Foods");
        }
    }
}
