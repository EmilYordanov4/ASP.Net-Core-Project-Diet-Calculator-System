using DietCalculatorSystem.Data;
using DietCalculatorSystem.Models.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Balanced([FromQuery] AllFoodsQueryModel query)
        {
            var foodsAsQuery = data.Foods.AsQueryable();


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
