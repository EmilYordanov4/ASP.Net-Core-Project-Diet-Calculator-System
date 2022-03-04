using DietCalculatorSystem.Models.Foods;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Models.Diets
{
    public class DietFormModel
    {
        public double? TotalCalories { get; set; }
        public double? TotalProteins { get; set; }
        public double? TotalFats { get; set; }
        public double? TotalCarbohydrates { get; set; }

        //Breakfast
        public double? BreakfastCalories { get; set; }
        public double? BreakfastProteins { get; set; }
        public double? BreakfastFats { get; set; }
        public double? BreakfastCarbohydrates { get; set; }

        //Lunch
        public double? LunchCalories { get; set; }
        public double? LunchProteins { get; set; }
        public double? LunchFats { get; set; }
        public double? LunchCarbohydrates { get; set; }

        //Dinner
        public double? DinnerCalories { get; set; }
        public double? DinnerProteins { get; set; }
        public double? DinnerFats { get; set; }
        public double? DinnerCarbohydrates { get; set; }

        //Search
        public const int FoodsPerPage = 4;

        public int CurrentPage { get; set; } = 1;

        public int TotalFoods { get; set; }

        public FoodSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<AllFoodsFormModel> Foods { get; set; }
    }
}
