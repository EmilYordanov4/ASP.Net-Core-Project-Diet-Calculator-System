using System.ComponentModel.DataAnnotations;

using static DietCalculatorSystem.Data.Models.DataConstants.Food;

namespace DietCalculatorSystem.Models.Foods
{

    public class AddFoodFormModel
    {
        [Required]
        [StringLength(MaxFoodNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinFoodNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }

        [Required]
        [Range(MinCalories, MaxCalories)]
        public int? Calories { get; set; }

        [Required]
        [Range(MinProteins, MaxProteins)]
        public double? Proteins { get; set; }

        [Required]
        [Range(MinFats, MaxFats)]
        public double? Fats { get; set; }

        [Required]
        [Range(MinCarbohydrates, MaxCarbohydrates)]
        public double? Carbohydrates { get; set; }
    }
}
