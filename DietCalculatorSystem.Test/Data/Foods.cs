using DietCalculatorSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace DietCalculatorSystem.Test.Data
{
    public static class Foods
    {
        public static IEnumerable<Food> TenPublicFoods
            => Enumerable.Range(0, 10).Select(i => new Food
            {
                IsPublic = true
            });
        
        public static Food FirstFood
            => new Food
            {
                Id = "1",
                IsPublic = true
            };
        public static Food SecondFood
            => new Food
            {
                Id = "2",
                IsPublic = true
            };
        public static Food ThirdFood
            => new Food
            {
                Id = "3",
                IsPublic = true
            };

    }
}
