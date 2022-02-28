using System;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    public class Diet
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //Total
        public double TotalCalories { get; set; }
        public double TotalProteins { get; set; }
        public double TotalFats { get; set; }
        public double TotalCarbohydrates { get; set; }

        //Breakfast
        public double BreakfastCalories { get; set; }
        public double BreakfastProteins { get; set; }
        public double BreakfastFats { get; set; }
        public double BreakfastCarbohydrates { get; set; }

        //Lunch
        public double LunchCalories { get; set; }
        public double LunchProteins { get; set; }
        public double LunchFats { get; set; }
        public double LunchCarbohydrates { get; set; }

        //Dinner
        public double DinnerCalories { get; set; }
        public double DinnerProteins { get; set; }
        public double DinnerFats { get; set; }
        public double DinnerCarbohydrates { get; set; }

    }
}
