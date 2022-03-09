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

        public ICollection<AllFoodsFormModel> TotalFoods { get; set; } = new List<AllFoodsFormModel>();

        //Breakfast
        public double? BreakfastCalories { get; set; }
        public double? BreakfastProteins { get; set; }
        public double? BreakfastFats { get; set; }
        public double? BreakfastCarbohydrates { get; set; }
        public ICollection<AllFoodsFormModel> BreakfastFoods { get; set; } = new List<AllFoodsFormModel>();

        //Lunch
        public double? LunchCalories { get; set; }
        public double? LunchProteins { get; set; }
        public double? LunchFats { get; set; }
        public double? LunchCarbohydrates { get; set; }
        public ICollection<AllFoodsFormModel> LunchFoods { get; set; } = new List<AllFoodsFormModel>();

        //Dinner
        public double? DinnerCalories { get; set; }
        public double? DinnerProteins { get; set; }
        public double? DinnerFats { get; set; }
        public double? DinnerCarbohydrates { get; set; }
        public ICollection<AllFoodsFormModel> DinnerFoods { get; set; } = new List<AllFoodsFormModel>();

        //Search
        public const int FoodsPerPage = 4;

        public int CurrentPage { get; set; } = 1;

        public int TotalFoodsCount { get; set; }

        public FoodSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<AllFoodsFormModel> Foods { get; set; }
    }
}
