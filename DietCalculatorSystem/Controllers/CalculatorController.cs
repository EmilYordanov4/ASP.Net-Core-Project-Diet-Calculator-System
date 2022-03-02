using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly DietCalculatorDbContext data;

        public CalculatorController(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Calculator()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Calculator(CalculateCaloriesFormModel calculateModel)
        {
            bool isModelValid = ValidateActivity(calculateModel.Activity) && ValidateGender(calculateModel.Gender);

            if (!isModelValid)
            {
                ModelState.AddModelError(nameof(calculateModel.Activity), "Invalid activity type");
            }

            if (!ModelState.IsValid)
            {
                return View(calculateModel);
            }

            var calories = calculateModel.Gender == "Male" ? CalculateMaleCalories(calculateModel) :
                                                             CalculateFemaleCalories(calculateModel);

            var percent = (double)(calories * 0.15);

            var user = data.Users.FirstOrDefault(x => x.FullName == this.User.Identity.Name);

            var balancedDiet = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            var surplusDiet = data
                .SurplusDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            var deficitDiet = data
                .DeficitDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            balancedDiet.TotalCalories = Math.Round((double)calories, 2);
            surplusDiet.TotalCalories = Math.Round((double)calories + percent, 2);
            deficitDiet.TotalCalories = Math.Round((double)calories - percent, 2);

            CalculateMacros(balancedDiet, calculateModel.Weight);
            CalculateMacros(surplusDiet, calculateModel.Weight);
            CalculateMacros(deficitDiet, calculateModel.Weight);

            SplitMeals(balancedDiet);
            SplitMeals(surplusDiet);
            SplitMeals(deficitDiet);

            data.SaveChanges();

            return Redirect("/Food/All");
        }

        private static void SplitMeals(Diet diet)
        {
            var mealCalories = Math.Round((double)diet.TotalCalories / 3, 2);
            var mealProteins = Math.Round((double)diet.TotalProteins / 3, 2);
            var mealFats = Math.Round((double)diet.TotalFats / 3, 2);
            var mealCarbohydrates = Math.Round((double)diet.TotalCarbohydrates / 3, 2);

            //Breakfast
            diet.BreakfastCalories = mealCalories;
            diet.BreakfastProteins = mealProteins;
            diet.BreakfastFats = mealFats;
            diet.BreakfastCarbohydrates = mealCarbohydrates;

            //Lunch
            diet.LunchCalories = mealCalories;
            diet.LunchProteins = mealProteins;
            diet.LunchFats = mealFats;
            diet.LunchCarbohydrates = mealCarbohydrates;

            //Dinner
            diet.DinnerCalories = mealCalories;
            diet.DinnerProteins = mealProteins;
            diet.DinnerFats = mealFats;
            diet.DinnerCarbohydrates = mealCarbohydrates;

        }

        private static void CalculateMacros(Diet diet, double? weight)
        {
            var caloriesLeft = diet.TotalCalories;

            diet.TotalProteins = Math.Round((double)weight * 1.8, 2);


            diet.TotalFats = Math.Round((double)(caloriesLeft * 0.20) / 9, 2);

            caloriesLeft -= diet.TotalProteins * 4;

            caloriesLeft -= diet.TotalFats * 9;

            diet.TotalCarbohydrates = Math.Round((double)caloriesLeft / 4, 2);
        }

        private static double? CalculateFemaleCalories(CalculateCaloriesFormModel calculateModel)
        {
            double? MSJEquation = ((10 * calculateModel.Weight) +
                                              (6.25 * calculateModel.Height) -
                                              (5 * calculateModel.Age) - 161) *
                                              calculateModel.Activity;

            double? RHBEquation = ((9.247 * calculateModel.Weight) +
                                  (3.098 * calculateModel.Height) -
                                  (4.330 * calculateModel.Age) + 447.593) *
                                  calculateModel.Activity;

            return (MSJEquation + RHBEquation) / 2;
        }

        private static double? CalculateMaleCalories(CalculateCaloriesFormModel calculateModel)
        {
            double? MSJEquation = ((10 * calculateModel.Weight) +
                                  (6.25 * calculateModel.Height) -
                                  (5 * calculateModel.Age) + 5) *
                                  calculateModel.Activity;

            double? RHBEquation = ((13.397 * calculateModel.Weight) +
                                  (4.799 * calculateModel.Height) -
                                  (5.677 * calculateModel.Age) + 88.362) *
                                  calculateModel.Activity;

            return (MSJEquation + RHBEquation) / 2;
        }

        private static bool ValidateGender(string gender)
        {
            return gender switch
            {
                "Male" => true,
                "Female" => true,
                _ => false
            };
        }

        private static bool ValidateActivity(double? activity)
        {
            return activity switch
            {
                1.2 => true,
                1.375 => true,
                1.55 => true,
                1.725 => true,
                _ => false
            };
        }
    }
}
