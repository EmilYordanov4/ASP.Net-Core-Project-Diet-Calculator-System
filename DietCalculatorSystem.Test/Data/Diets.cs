using DietCalculatorSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static DietCalculatorSystem.Test.Data.Foods;

namespace DietCalculatorSystem.Test.Data
{
    public static class Diets
    {
        public static Diet FirstDiet
            => new Diet
            {
                Id = "1",
            };
    }
}
