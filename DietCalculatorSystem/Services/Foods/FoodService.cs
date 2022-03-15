using DietCalculatorSystem.Data;
using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods.Models;
using System.Linq;

namespace DietCalculatorSystem.Services.Foods
{
    public class FoodService : IFoodService
    {
        private readonly DietCalculatorDbContext data;

        public FoodService(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        public FoodQueryServiceModel All(int foodsPerPage = 5,
            int currentPage = 1,
            string searchTerm = null,
            FoodSorting foodSorting = FoodSorting.Calories)
        {
            var foodsAsQuery = data.Foods.AsQueryable();


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                foodsAsQuery = foodsAsQuery
                    .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            foodsAsQuery = foodSorting switch
            {
                FoodSorting.Proteins => foodsAsQuery.OrderByDescending(x => x.Proteins),
                FoodSorting.Fats => foodsAsQuery.OrderByDescending(x => x.Fats),
                FoodSorting.Carbohydrates => foodsAsQuery.OrderByDescending(x => x.Carbohydrates),
                _ => foodsAsQuery.OrderByDescending(x => x.Calories),
            };

            var totalFoods = foodsAsQuery.Count();

            var allfoods = foodsAsQuery
                .Skip((currentPage - 1) * foodsPerPage)
                .Take(foodsPerPage)
                .Select(x => new FoodServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    Calories = x.Calories,
                    Proteins = x.Proteins,
                    Fats = x.Fats,
                    Carbohydrates = x.Carbohydrates,
                })
                .ToList();

            return new FoodQueryServiceModel
            {
                CurrentPage = currentPage,
                TotalFoods = totalFoods,
                FoodsPerPage = foodsPerPage,
                Foods = allfoods,
            };
        }
    }
}
