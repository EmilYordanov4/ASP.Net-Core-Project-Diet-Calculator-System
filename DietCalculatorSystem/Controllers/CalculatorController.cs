using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using DietCalculatorSystem.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var user = data.Users.FirstOrDefault(x => x.FullName == this.User.Identity.Name);

            var diet = new Diet
            {
                TotalCalories = (double)calories,
            };

            var balancedDiet = new BalancedDiet
            {
                User = user,
                UserId = user.Id,
                Diet = diet,
                DietId = diet.Id,
            };

            data.Diets.Add(diet);

            data.BalancedDiets.Add(balancedDiet);
            //Decifit
            //Surplus

            data.SaveChanges();

            return Redirect("/Food/All");
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
