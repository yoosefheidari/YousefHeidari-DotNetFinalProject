using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
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
        private readonly ICategoryAppService _categoryAppService;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(IUserAppService userAppService, ILogger<UserManagementController> logger, ICategoryAppService categoryAppService)
        {
            _userAppService = userAppService;
            _logger = logger;
            _categoryAppService = categoryAppService;
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
        public async Task<IActionResult> Edit(UserDTO userDTO, string? oldPassword, string? newPassword, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            await _userAppService.Update(userDTO, oldPassword, newPassword,cancellationToken);
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
        public async Task<IActionResult> EditExpertSkill(int id, CancellationToken cancellationToken)
        {
            var user = await _userAppService.Get(id);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Where(t => !user.expertCategories.Any(r => r.Name == t.Name)).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.UserId = user.Id;
            var userCats = user.expertCategories;
            return View(userCats);
        }
        [HttpPost]
        public async Task<IActionResult> EditExpertSkill(List<int> categories, int userId, CancellationToken cancellationToken)
        {
            await _userAppService.UpdateExpertSkills(userId, categories, cancellationToken);
            return RedirectToAction("Index");
        }

    }
}
