using DietCalculatorSystem.Data.Models;

namespace DietCalculatorSystem.Models.Foods
{
    public class DetailedFoodFormModel
    {
        public Food MainFood { get; set; }
        public Food FirstSuggestedFood { get; set; }
        public Food SecondSuggestedFood { get; set; }


    }
}
