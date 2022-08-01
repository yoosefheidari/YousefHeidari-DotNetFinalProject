using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
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
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderAppService _orderAppService;

        public HomeController(ILogger<HomeController> logger, ICategoryAppService categoryAppService, IServiceAppService serviceAppService, IOrderAppService orderAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
            _serviceAppService = serviceAppService;
            _orderAppService = orderAppService;
        }



        public IActionResult Index(int id, CancellationToken cancellationToken)
        {
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("LoginUser");
        }

        public async Task<IActionResult> GetListofServices(int id, CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(id, cancellationToken);
            if (id == 0)
                services = null;
            return View("Services", services);
        }




        public async Task<IActionResult> UserOrder(string name, CancellationToken cancellationToken)
        {

            var categories = await _categoryAppService.GetAllWithServices(cancellationToken);
            return View(categories);
        }
        public IActionResult NewOrder(int id)
        {

            ViewBag.ServiceId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(OrderDTO order, List<IFormFile> files, CancellationToken cancellationToken)
        {

            var result = await _orderAppService.AddNewOrder(order, files, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }

        public IActionResult AccessDenied()
        {


            return View();
        }

    }
}