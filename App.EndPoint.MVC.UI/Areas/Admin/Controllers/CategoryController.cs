
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            var result = await _categoryAppService.Add(categoryDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id,CancellationToken cancellationToken)
        {
            var category = await _categoryAppService.Get(id, cancellationToken);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            await _categoryAppService.Update(categoryDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            await _categoryAppService.Delete(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
