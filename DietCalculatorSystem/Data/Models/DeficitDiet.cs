using System;
using System.Collections.Generic;

namespace DietCalculatorSystem.Data.Models
{
    public class DeficitDiet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int TotalCalories { get; set; }
        public int TotalProteins { get; set; }
        public int TotalFats { get; set; }
        public int TotalCarbohydrates { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        //Breakfast
        public List<Food> BreakfastFoods { get; set; } = new List<Food>();
        public int BreakfastCalories { get; set; }
        public int BreakfastProteins { get; set; }
        public int BreakfastFats { get; set; }
        public int BreakfastCarbohydrates { get; set; }

        //Lunch
        public List<Food> LunchFoods { get; init; } = new List<Food>();
        public int LunchCalories { get; set; }
        public int LunchProteins { get; set; }
        public int LunchFats { get; set; }
        public int LunchCarbohydrates { get; set; }

        //Dinner
        public List<Food> DinnerFoods { get; init; } = new List<Food>();
        public int DinnerCalories { get; set; }
        public int DinnerProteins { get; set; }
        public int DinnerFats { get; set; }
        public int DinnerCarbohydrates { get; set; }

        //Snacks
        public List<Food> SnackFoods { get; init; } = new List<Food>();
        public int SnackCalories { get; set; }
        public int SnackProteins { get; set; }
        public int SnackFats { get; set; }
        public int SnackCarbohydrates { get; set; }
    }
}
