using DietCalculatorSystem.Data;
using DietCalculatorSystem.Models.Diets;
using DietCalculatorSystem.Models.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
    public class DietController : Controller
    {
        private readonly DietCalculatorDbContext data;

        public DietController(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Balanced([FromQuery] DietFormModel query)
        {
            var foodsAsQuery = data.Foods.AsQueryable();

            var balancedDiet = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(a => a.User.FullName == this.User.Identity.Name);

            //Total
            query.TotalCalories = balancedDiet.Diet.TotalCalories;
            query.TotalProteins = balancedDiet.Diet.TotalProteins;
            query.TotalFats = balancedDiet.Diet.TotalFats;
            query.TotalCarbohydrates = balancedDiet.Diet.TotalCarbohydrates;

            //Breakfast
            query.BreakfastCalories = balancedDiet.Diet.BreakfastCalories;
            query.BreakfastProteins = balancedDiet.Diet.BreakfastProteins;
            query.BreakfastFats = balancedDiet.Diet.BreakfastFats;
            query.BreakfastCarbohydrates = balancedDiet.Diet.BreakfastCarbohydrates;

            //Lunch
            query.LunchCalories = balancedDiet.Diet.LunchCalories;
            query.LunchProteins = balancedDiet.Diet.LunchProteins;
            query.LunchFats = balancedDiet.Diet.LunchFats;
            query.LunchCarbohydrates = balancedDiet.Diet.LunchCarbohydrates;

            //Dinner
            query.DinnerCalories = balancedDiet.Diet.DinnerCalories;
            query.DinnerProteins = balancedDiet.Diet.DinnerProteins;
            query.DinnerFats = balancedDiet.Diet.DinnerFats;
            query.DinnerCarbohydrates = balancedDiet.Diet.DinnerCarbohydrates;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                foodsAsQuery = foodsAsQuery
                    .Where(x => x.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            int totalFoods = foodsAsQuery.Count();

            foodsAsQuery = query.Sorting switch
            {
                FoodSorting.Proteins => foodsAsQuery.OrderByDescending(x => x.Proteins),
                FoodSorting.Fats => foodsAsQuery.OrderByDescending(x => x.Fats),
                FoodSorting.Carbohydrates => foodsAsQuery.OrderByDescending(x => x.Carbohydrates),
                _ => foodsAsQuery.OrderByDescending(x => x.Calories),
            };

            query.Foods = foodsAsQuery
                .Skip((query.CurrentPage - 1) * AllFoodsQueryModel.FoodsPerPage)
                .Take(AllFoodsQueryModel.FoodsPerPage)
                .Select(x => new AllFoodsFormModel
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

            query.TotalFoods = totalFoods;

            return View(query);
        }
    }
}
