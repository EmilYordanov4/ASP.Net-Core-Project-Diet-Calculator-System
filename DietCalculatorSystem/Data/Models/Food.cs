using System;

namespace DietCalculatorSystem.Data.Models
{
    public class Food
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public int Calories { get; set; }

        public int Proteins { get; set; }

        public int Fats { get; set; }

        public int Carbohydrates { get; set; }
    }
}
