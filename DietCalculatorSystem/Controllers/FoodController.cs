using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
	public class FoodController : Controller
    {
        private const int foodsPerPage = 8;

        private readonly DietCalculatorDbContext data;
        private readonly IFoodService foods;

        public FoodController(DietCalculatorDbContext data,
            IFoodService foods)
        {
            this.data = data;
            this.foods = foods;
        }

        public IActionResult All([FromQuery] AllFoodsQueryModel query)
        {
            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddFoodFormModel foodModel)
        {
            if (data.Foods.Any(x => x.Name == foodModel.Name))
            {
                this.ModelState.AddModelError(nameof(foodModel.Name), "Food already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(foodModel);
            }

            Food food = new()
            {
                Name = foodModel.Name,
                Calories = foodModel.Calories,
                Proteins = foodModel.Proteins,
                Carbohydrates = foodModel.Carbohydrates,
                Fats = foodModel.Fats,
                Description = foodModel.Description,
                PictureUrl = foodModel.PictureUrl,
            };

            data.Foods.Add(food);

            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(string id) 
        {
            var allFoods = data
                .Foods
                .ToList();

            var mainFood = allFoods
                .FirstOrDefault(x => x.Id == id);

            allFoods.Remove(mainFood);

            Random rnd = new Random();

            var suggestedFoodOne = allFoods[rnd.Next(0, allFoods.Count())];

            allFoods.Remove(suggestedFoodOne);

            var suggestedFoodTwo = allFoods[rnd.Next(0, allFoods.Count())];

            var foods = new DetailedFoodFormModel
            {
                MainFood = mainFood,
                FirstSuggestedFood = suggestedFoodOne,
                SecondSuggestedFood = suggestedFoodTwo
            };

            return this.View(foods);
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var food = data.Foods.FirstOrDefault(x => x.Id == id);

            data.Foods.Remove(food);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
