using App.Domain.Core.Work.Contracts.AppServices;
using App.EndPoint.MVC.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.EndPoint.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryAppService _categoryAppService;

        public HomeController(ILogger<HomeController> logger, ICategoryAppService categoryAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAllWithServices(cancellationToken);
            return View(categories);
        }

        public IActionResult Privacy()
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