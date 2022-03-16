using DietCalculatorSystem.Models;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DietCalculatorSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodService foods;

        public HomeController(IFoodService foods)
        {
            this.foods = foods;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(IndexLoggedIn));
            }

            return View();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            return View(foods.GetRandomFood());
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
