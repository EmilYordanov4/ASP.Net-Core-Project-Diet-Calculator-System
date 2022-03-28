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
    }
}
