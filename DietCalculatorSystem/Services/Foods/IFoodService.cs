using DietCalculatorSystem.Data.Models;
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

        bool FoodExists(string foodName);

        void CreateFood(string name,
            double? calories,
            double? proteins,
            double? fats,
            double? carbohydrates,
            string description,
            string pictureUrl);

        FoodDetailsServiceModel GetDetails(string foodId);

        void RemoveFood(string foodId);

        Food GetRandomFood();
    }
}
