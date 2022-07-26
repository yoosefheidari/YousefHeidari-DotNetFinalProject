using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace App.EndPoint.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;

        public AccountController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IActionResult> Profile(string name)
        {
            var user = await _userAppService.GetUserByUserName(name);
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, bool remember)
        {
            var result = await _userAppService.LoginUser(userName, password, remember);
            

            if (result==0)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var user = await _userAppService.Get(result);
                if (user.Roles.Count == 1)
                {
                    return RedirectToAction(nameof(HomeController.Index));
                }
                else
                {
                    return View("PanelSelect",user.Roles);
                }
            }

        }


        public async Task<IActionResult> Logout()
        {
            await _userAppService.SignoutUser();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO, string password, List<IFormFile> files,CancellationToken cancellationToken)
                       
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            var id = await _userAppService.RegisterUser(userDTO, password,files,cancellationToken);
            await _userAppService.SignInUserById(id);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
