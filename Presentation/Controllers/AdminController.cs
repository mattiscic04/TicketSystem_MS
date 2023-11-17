using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {

                TempData["Error"] = "Access Denied";

                return RedirectToAction("Error", "Home");
            }


            return View();
        }
    }
}
