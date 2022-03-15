using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods.Models;

namespace DietCalculatorSystem.Services.Foods
{
    public interface IFoodService
    {
        FoodQueryServiceModel All(int foodsPerPage = 5,
            int currentPage = 1,
            string searchTerm = null,
            FoodSorting foodSorting = FoodSorting.Calories);
    }
}
