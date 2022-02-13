using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Models.Foods
{
    public class AllFoodsQueryModel
    {
        public const int FoodsPerPage = 4;

        public int CurrentPage { get; set; } = 1;

        public int TotalFoods { get; set; }

        public FoodSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<AllFoodsFormModel> Foods { get; set; }
    }
}
