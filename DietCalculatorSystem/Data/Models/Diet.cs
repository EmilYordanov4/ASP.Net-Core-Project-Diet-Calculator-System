using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietCalculatorSystem.Data.Models
{
    public class Diet
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //Total
        public int TotalCalories { get; set; }
        public int TotalProteins { get; set; }
        public int TotalFats { get; set; }
        public int TotalCarbohydrates { get; set; }

        //Breakfast
        public int BreakfastCalories { get; set; }
        public int BreakfastProteins { get; set; }
        public int BreakfastFats { get; set; }
        public int BreakfastCarbohydrates { get; set; }

        //Lunch
        public int LunchCalories { get; set; }
        public int LunchProteins { get; set; }
        public int LunchFats { get; set; }
        public int LunchCarbohydrates { get; set; }

        //Dinner
        public int DinnerCalories { get; set; }
        public int DinnerProteins { get; set; }
        public int DinnerFats { get; set; }
        public int DinnerCarbohydrates { get; set; }

    }
}
