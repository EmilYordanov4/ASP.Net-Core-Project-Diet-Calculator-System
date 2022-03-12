using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using DietCalculatorSystem.Models.Diets;
using DietCalculatorSystem.Models.Foods;
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
        private readonly DietCalculatorDbContext data;

        public DietController(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        public IActionResult Balanced([FromQuery] DietFormModel query)
        {
            var foodsAsQuery = data.Foods.AsQueryable();

            var balancedDiet = data
                .BalancedDiets
                .Include(a => a.Diet)
                .ThenInclude(d => d.BreakfastFoods)
                .ThenInclude(bf => bf.Food)
                .Include(a => a.Diet)
                .ThenInclude(d => d.LunchFoods)
                .ThenInclude(lf => lf.Food)
                .Include(a => a.Diet)
                .ThenInclude(d => d.DinnerFoods)
                .ThenInclude(df => df.Food)
                .FirstOrDefault(a => a.User.FullName == this.User.Identity.Name);

            query.DietId = balancedDiet.Diet.Id;

            //Breakfast
            query.BreakfastCalories = balancedDiet.Diet.BreakfastCalories;
            query.BreakfastProteins = balancedDiet.Diet.BreakfastProteins;
            query.BreakfastFats = balancedDiet.Diet.BreakfastFats;
            query.BreakfastCarbohydrates = balancedDiet.Diet.BreakfastCarbohydrates;
            query.BreakfastFoods = balancedDiet
                                    .Diet
                                    .BreakfastFoods
                                    .Select(a => new AllFoodsFormModel
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
            query.LunchCalories = balancedDiet.Diet.LunchCalories;
            query.LunchProteins = balancedDiet.Diet.LunchProteins;
            query.LunchFats = balancedDiet.Diet.LunchFats;
            query.LunchCarbohydrates = balancedDiet.Diet.LunchCarbohydrates;
            query.LunchFoods = balancedDiet
                                    .Diet
                                    .LunchFoods
                                    .Select(a => new AllFoodsFormModel
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
            query.DinnerCalories = balancedDiet.Diet.DinnerCalories;
            query.DinnerProteins = balancedDiet.Diet.DinnerProteins;
            query.DinnerFats = balancedDiet.Diet.DinnerFats;
            query.DinnerCarbohydrates = balancedDiet.Diet.DinnerCarbohydrates;
            query.DinnerFoods = balancedDiet
                                    .Diet
                                    .DinnerFoods
                                    .Select(a => new AllFoodsFormModel
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
            query.TotalCalories = Math.Round((double)(balancedDiet.Diet.TotalCalories - consumedCalories),2);
            query.TotalProteins = Math.Round((double)(balancedDiet.Diet.TotalProteins - consumedProteins), 2);
            query.TotalFats = Math.Round((double)(balancedDiet.Diet.TotalFats - consumedFats), 2);
            query.TotalCarbohydrates = Math.Round((double)(balancedDiet.Diet.TotalCarbohydrates - consumedCarbohydrates), 2);

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
                .Skip((query.CurrentPage - 1) * DietFormModel.FoodsPerPage)
                .Take(DietFormModel.FoodsPerPage)
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

            query.TotalFoodsCount = totalFoods;

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
