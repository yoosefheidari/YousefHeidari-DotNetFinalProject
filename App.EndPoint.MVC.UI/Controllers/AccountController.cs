﻿using App.Domain.Core.User.Contracts.AppServices;
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
            var user = await _userAppService.GetCurrentUserFullInfo();
            return View(user);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO user, bool remember)
        {
            var result = await _userAppService.LoginUser(user.UserName, user.Password, remember);


            if (result == 0)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var loginUser = await _userAppService.Get(result);
                if (loginUser.Roles.Count == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("PanelSelect", loginUser.Roles);
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO, CancellationToken cancellationToken)

        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            var id = await _userAppService.RegisterUser(userDTO, userDTO.Password, cancellationToken);
            await _userAppService.SignInUserById(id);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        public async Task<IActionResult> EditProfile(CancellationToken cancellationToken)
        {
            var user = await _userAppService.GetCurrentUserFullInfo();
            var categories = await _categoryAppService.GetAll(cancellationToken);
            var filteredCategories = categories.Where(t => !user.expertCategories
                .Any(r => r.Name == t.Name))
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
                .ToList();
            ViewBag.Categories = filteredCategories;
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDTO userDTO, List<int>? categories, string? oldPassword, string? newPassword, IFormFile? file, CancellationToken cancellationToken)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            await _userAppService.Update(userDTO, oldPassword, newPassword, cancellationToken);
            await _userAppService.UpdateExpertSkills(userDTO.Id, categories, cancellationToken);
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile? file, CancellationToken cancellationToken)
        {
            await _userAppService.ChangeProfilePicture(file, cancellationToken);
            return RedirectToAction("Profile", "Account");
        }


    }
}
