using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietCalculatorSystem.Controllers
{
    public class DietController : Controller
    {
        [Authorize]
        public IActionResult Balanced()
        {
            return View();
        }
    }
}
