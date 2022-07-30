using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =("Admin"))]
    public class HomeController : Controller
    {
        private readonly IUtilityAppService _utilityAppService;

        public HomeController(IUtilityAppService utilityAppService)
        {
            _utilityAppService = utilityAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            ViewBag.Statistics = await _utilityAppService.GetLatestStatistics(cancellationToken);
            return View();
        }
    }
}
