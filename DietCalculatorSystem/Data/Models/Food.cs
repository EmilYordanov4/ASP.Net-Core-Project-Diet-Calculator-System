using System;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    public class Food
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public int Calories { get; set; }

        public int Proteins { get; set; }

        public int Fats { get; set; }

        public int Carbohydrates { get; set; }
    }
}
