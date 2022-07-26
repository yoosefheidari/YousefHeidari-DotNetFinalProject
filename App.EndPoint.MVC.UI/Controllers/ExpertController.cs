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
        private readonly IHttpContextAccessor _httpContext;
        private readonly IOrderAppService _orderAppService;

        public ExpertController(IUserAppService userAppService, IHttpContextAccessor httpContext, IOrderAppService orderAppService)
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
            var currentUserUsername = _httpContext.HttpContext.User.Identity.Name;
            var expert =await _userAppService.GetUserByUserName(currentUserUsername);
            var orders = _orderAppService.GetAllExpertOrders(expert, query,cancellationToken);

            return View(orders);
        }

        public IActionResult Indfex()
        {
            ViewData["Message"] = "Expert";
            return View();
        }
    }
}
