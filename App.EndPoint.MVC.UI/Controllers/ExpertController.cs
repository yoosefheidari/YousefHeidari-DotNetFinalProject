using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Controllers
{
    [Authorize(Roles ="Admin,Expert")]

    public class ExpertController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly HttpContext _httpContext;
        private readonly IOrderAppService _orderAppService;

        public ExpertController(IUserAppService userAppService, HttpContext httpContext, IOrderAppService orderAppService)
        {
            _userAppService = userAppService;
            _httpContext = httpContext;
            _orderAppService = orderAppService;
        }

        public IActionResult Index()
        {            
            ViewData["Message"] = "Expert";
            return View();            
        }

        public async Task<IActionResult> Orders(string name,CancellationToken cancellationToken)
        {
            string query = name;
            ViewData["Message"] = "Expert";
            var currentUserUsername = _httpContext.User.Identity.Name;
            var expert =await _userAppService.GetUserByUserName(currentUserUsername);
            var expertId= expert.Id;
            var orders = _orderAppService.GetAllExpertOrders(expertId,query,cancellationToken);

            return View(orders);
        }

        public IActionResult Indfex()
        {
            ViewData["Message"] = "Expert";
            return View();
        }
    }
}
