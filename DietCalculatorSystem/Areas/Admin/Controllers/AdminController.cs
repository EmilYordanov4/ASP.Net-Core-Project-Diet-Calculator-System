using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietCalculatorSystem.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
