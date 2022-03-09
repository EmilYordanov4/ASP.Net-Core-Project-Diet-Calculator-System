﻿using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models.ManyToManyRelationships
{
    public class TotalFood
    {
        [Key]
        [Required]
        public string DietId { get; set; }

        public Diet Diet { get; set; }

        [Key]
        [Required]
        public string FoodId { get; set; }
        public Food Food { get; set; }
    }
}
