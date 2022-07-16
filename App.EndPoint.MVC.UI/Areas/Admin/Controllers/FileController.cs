using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
