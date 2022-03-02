using DietCalculatorSystem.Data;
using DietCalculatorSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DietCalculatorDbContext data;

        public HomeController(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("IndexLoggedIn");
            }

            return View();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            var allFoods = data
                .Foods
                .ToList();

            var count = allFoods.Count();

            Random rnd = new Random();

            var food = allFoods[rnd.Next(0, count)];

            return View(food);
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
