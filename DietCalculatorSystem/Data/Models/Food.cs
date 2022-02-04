using System;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    public class Food
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int Calories { get; set; }

        [Required]
        public int Proteins { get; set; }

        [Required]
        public int Fats { get; set; }

        [Required]
        public int Carbohydrates { get; set; }
    }
}
