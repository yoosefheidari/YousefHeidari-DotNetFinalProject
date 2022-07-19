using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = User.Identity.Name;
                return View();
            }

            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

        }
    }
}
