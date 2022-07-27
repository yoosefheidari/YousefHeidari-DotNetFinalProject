﻿using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(IUserAppService userAppService, ILogger<UserManagementController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id, string? search, CancellationToken cancellationToken)
        {
            var users = await _userAppService.GetAll(id, search, cancellationToken);
            return View(users);

        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var roles = await _userAppService.GetRoles();            
            var user = await _userAppService.Get(id);
            ViewBag.Roles = roles.Where(t => !user.Roles.Contains(t.Name)).Select(x => new SelectListItem() { Value = x.Name, Text = x.Name }).ToList();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO userDTO,string oldPassword,string newPassword ,CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of {actionName} user", "Update");
            await _userAppService.Update(userDTO,oldPassword,newPassword);
            _logger.LogTrace("end of {actionName} user", "Update");
            _logger.LogInformation("User {Action} progress done Successfully", "Update");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _userAppService.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDTO userDTO, CancellationToken cancellationToken)
        {
            await _userAppService.Delete(userDTO.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken)
        {
            var user = await _userAppService.Get(id);
            return View(user);
        }
        //public async Task<IActionResult> SeedData(int id, CancellationToken cancellationToken)
        //{
        //    var user = await _userAppService.Get(id);
        //    return View(user);
        //}


    }
}
