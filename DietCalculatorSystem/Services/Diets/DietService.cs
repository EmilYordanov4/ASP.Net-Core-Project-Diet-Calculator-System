using DietCalculatorSystem.Data;
using DietCalculatorSystem.Services.Diets.Models;
using DietCalculatorSystem.Services.Foods.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DietCalculatorSystem.Services.Diets
{
    public class DietService : IDietService
    {
        private readonly DietCalculatorDbContext data;

        public DietService(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        public DietServiceModel GetAllMeals(string dietId)
        {
            var diet = data
                .Diets
                .Include(d => d.BreakfastFoods)
                .ThenInclude(bf => bf.Food)
                .Include(d => d.LunchFoods)
                .ThenInclude(lf => lf.Food)
                .Include(d => d.DinnerFoods)
                .ThenInclude(df => df.Food)
                .FirstOrDefault(d => d.Id == dietId);

            //Breakfast
            var breakfastFoods = diet
                .BreakfastFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            //Lunch
            var lunchFoods = diet
                .LunchFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            //Dinner
            var dinnerFoods = diet
                .DinnerFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            return new DietServiceModel
            {
                TotalCalories = diet.TotalCalories,
                TotalProteins = diet.TotalProteins,
                TotalFats = diet.TotalFats,
                TotalCarbohydrates = diet.TotalCarbohydrates,
                BreakfastFoods = breakfastFoods,
                DinnerFoods = dinnerFoods,
                LunchFoods = lunchFoods,
            };
        }

        public string GetDietId(string name, string methodName)
        {
            if (methodName == "Balanced")
            {
                return this.data
                    .BalancedDiets
                    .FirstOrDefault(x => x.User.FullName == name)
                    .DietId;
            }
            else if (methodName == "Deficit")
            {
                return this.data
                   .DeficitDiets
                   .FirstOrDefault(x => x.User.FullName == name)
                   .DietId;
            }
            else
            {
                return this.data
                   .SurplusDiets
                   .FirstOrDefault(x => x.User.FullName == name)
                   .DietId;
            }
        }
    }
}
