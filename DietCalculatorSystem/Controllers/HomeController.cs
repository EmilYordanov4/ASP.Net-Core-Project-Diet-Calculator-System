using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using DietCalculatorSystem.Models;
using DietCalculatorSystem.Models.Home;
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
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
