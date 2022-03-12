namespace DietCalculatorSystem.Models.Foods
{
	public class AllFoodsFormModel
	{
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public double? Calories { get; set; }

        public double? Proteins { get; set; }

        public double? Fats { get; set; }

        public double? Carbohydrates { get; set; }

        public int Quantity { get; set; }
    }
}
