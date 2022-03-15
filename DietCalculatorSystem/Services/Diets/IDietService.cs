using DietCalculatorSystem.Services.Diets.Models;

namespace DietCalculatorSystem.Services.Diets
{
    public interface IDietService
    {
        string GetDietId(string name, string methodName);

        DietServiceModel GetAllMeals(string dietId);
    }
}
