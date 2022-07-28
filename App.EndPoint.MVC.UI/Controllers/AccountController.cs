using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ICategoryAppService _categoryAppService;

        public AccountController(IUserAppService userAppService, ICategoryAppService categoryAppService)
        {
            _userAppService = userAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Profile()
        {
            ViewData["Message"] = "Profile";
            var user = await _userAppService.GetCurrentUser();
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


            if (result == 0)
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
                    return View("PanelSelect", user.Roles);
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
        public async Task<IActionResult> Register(UserDTO userDTO, string password, CancellationToken cancellationToken)

        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            var id = await _userAppService.RegisterUser(userDTO, password, cancellationToken);
            await _userAppService.SignInUserById(id);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public async Task<IActionResult> EditProfile(CancellationToken cancellationToken)
        {
            ViewData["Message"] = "Profile";
            var user = await _userAppService.GetCurrentUser();
            var categories = await _categoryAppService.GetAll(cancellationToken);

            var t = categories.Where(t => !user.expertCategories
            .Any(r => r.Name == t.Name))
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
                .ToList();
            ViewBag.Categories = t;
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDTO userDTO, List<int> categories, string? oldPassword, string? newPassword,IFormFile file, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            await _userAppService.Update(userDTO, oldPassword, newPassword, cancellationToken);
            await _userAppService.UpdateExpertSkills(userDTO.Id, categories, cancellationToken);
            await _userAppService.UpdateProfilePicture(userDTO.Id, file, cancellationToken);

            return RedirectToAction(nameof(Profile));
        }


    }
}
