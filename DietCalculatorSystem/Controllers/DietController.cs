using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using DietCalculatorSystem.Models.Diets;
using DietCalculatorSystem.Services.Diets;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
    [Authorize]
    public class DietController : Controller
    {
        private const int foodsPerPage = 5;

        private readonly DietCalculatorDbContext data;
        private readonly IFoodService foods;
        private readonly IDietService diets;

        public DietController(DietCalculatorDbContext data,
            IFoodService foods,
            IDietService diets)
        {
            this.data = data;
            this.foods = foods;
            this.diets = diets;
        }

        public IActionResult Surplus([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Surplus));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            //Math
            var consumedCalories =
                query.BreakfastFoods.Sum(x => x.Calories * x.Quantity) +
                query.LunchFoods.Sum(x => x.Calories * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Calories * x.Quantity);

            var consumedProteins = query.BreakfastFoods.Sum(x => x.Proteins * x.Quantity) +
                query.LunchFoods.Sum(x => x.Proteins * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Proteins * x.Quantity);

            var consumedFats = query.BreakfastFoods.Sum(x => x.Fats * x.Quantity) +
                query.LunchFoods.Sum(x => x.Fats * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Fats * x.Quantity);

            var consumedCarbohydrates = query.BreakfastFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.LunchFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Carbohydrates * x.Quantity);

            //Total
            query.TotalCalories = Math.Round((double)(diet.TotalCalories - consumedCalories), 2);
            query.TotalProteins = Math.Round((double)(diet.TotalProteins - consumedProteins), 2);
            query.TotalFats = Math.Round((double)(diet.TotalFats - consumedFats), 2);
            query.TotalCarbohydrates = Math.Round((double)(diet.TotalCarbohydrates - consumedCarbohydrates), 2);

            //Query
            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        public IActionResult Deficit([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Deficit));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            //Math
            var consumedCalories =
                query.BreakfastFoods.Sum(x => x.Calories * x.Quantity) +
                query.LunchFoods.Sum(x => x.Calories * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Calories * x.Quantity);

            var consumedProteins = query.BreakfastFoods.Sum(x => x.Proteins * x.Quantity) +
                query.LunchFoods.Sum(x => x.Proteins * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Proteins * x.Quantity);

            var consumedFats = query.BreakfastFoods.Sum(x => x.Fats * x.Quantity) +
                query.LunchFoods.Sum(x => x.Fats * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Fats * x.Quantity);

            var consumedCarbohydrates = query.BreakfastFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.LunchFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Carbohydrates * x.Quantity);

            //Total
            query.TotalCalories = Math.Round((double)(diet.TotalCalories - consumedCalories), 2);
            query.TotalProteins = Math.Round((double)(diet.TotalProteins - consumedProteins), 2);
            query.TotalFats = Math.Round((double)(diet.TotalFats - consumedFats), 2);
            query.TotalCarbohydrates = Math.Round((double)(diet.TotalCarbohydrates - consumedCarbohydrates), 2);

            //Query
            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        public IActionResult Balanced([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Balanced));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            //Math
            var consumedCalories = 
                query.BreakfastFoods.Sum(x => x.Calories * x.Quantity) +
                query.LunchFoods.Sum(x => x.Calories * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Calories * x.Quantity);

            var consumedProteins = query.BreakfastFoods.Sum(x => x.Proteins * x.Quantity) +
                query.LunchFoods.Sum(x => x.Proteins * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Proteins * x.Quantity);

            var consumedFats = query.BreakfastFoods.Sum(x => x.Fats * x.Quantity) +
                query.LunchFoods.Sum(x => x.Fats * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Fats * x.Quantity);

            var consumedCarbohydrates = query.BreakfastFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.LunchFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Carbohydrates * x.Quantity);

            //Total
            query.TotalCalories = Math.Round((double)(diet.TotalCalories - consumedCalories),2);
            query.TotalProteins = Math.Round((double)(diet.TotalProteins - consumedProteins), 2);
            query.TotalFats = Math.Round((double)(diet.TotalFats - consumedFats), 2);
            query.TotalCarbohydrates = Math.Round((double)(diet.TotalCarbohydrates - consumedCarbohydrates), 2);

            //Query
            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        public IActionResult AddBreakfast(string Id)
        {
            var Balanced = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(a => a.User.FullName == this.User.Identity.Name);

            var food = data
                .Foods
                .FirstOrDefault(a => a.Id == Id);

            var Food = data
                .BreakfastFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == Balanced.DietId);

            if (Food == null)
            {
                Food = new BreakfastFood
                {
                    Diet = Balanced.Diet,
                    DietId = Balanced.DietId,
                    Food = food,
                    FoodId = Id
                };

                Balanced.Diet.BreakfastFoods.Add(Food);
                data.BreakfastFoods.Add(Food);
            }

            Food.Quantity++;

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }

        public IActionResult AddLunch(string Id)
        {
            var Balanced = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(a => a.User.FullName == this.User.Identity.Name);

            var food = data
                .Foods
                .FirstOrDefault(a => a.Id == Id);

            var Food = data
                .LunchFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == Balanced.DietId);

            if (Food == null)
            {
                Food = new LunchFood
                {
                    Diet = Balanced.Diet,
                    DietId = Balanced.DietId,
                    Food = food,
                    FoodId = Id
                };

                Balanced.Diet.LunchFoods.Add(Food);
                data.LunchFoods.Add(Food);
            }

            Food.Quantity++;

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }

        public IActionResult AddDinner(string Id)
        {
            var Balanced = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(a => a.User.FullName == this.User.Identity.Name);

            var food = data
                .Foods
                .FirstOrDefault(a => a.Id == Id);

            var Food = data
                .DinnerFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == Balanced.DietId);

            if (Food == null)
            {
                Food = new DinnerFood
                {
                    Diet = Balanced.Diet,
                    DietId = Balanced.DietId,
                    Food = food,
                    FoodId = Id
                };

                Balanced.Diet.DinnerFoods.Add(Food);
                data.DinnerFoods.Add(Food);
            }

            Food.Quantity++;

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }

        public IActionResult RemoveBreakfast(string foodId, string dietId) 
        {
            var breakfastFood = data
                .BreakfastFoods
                .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            breakfastFood.Quantity--;

            if (breakfastFood.Quantity == 0)
            {
                data.BreakfastFoods.Remove(breakfastFood);
            }

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }
        
        public IActionResult RemoveLunch(string foodId, string dietId) 
        {
            var lunchFood = data
                .LunchFoods
                .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            lunchFood.Quantity--;

            if (lunchFood.Quantity == 0)
            {
                data.LunchFoods.Remove(lunchFood);
            }

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }
        
        public IActionResult RemoveDinner(string foodId, string dietId) 
        {
            var dinnerFood = data
                .DinnerFoods
                .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            dinnerFood.Quantity--;

            if (dinnerFood.Quantity == 0)
            {
                data.DinnerFoods.Remove(dinnerFood);
            }

            data.SaveChanges();

            return Redirect("/Diet/Balanced");
        }
    }
}
