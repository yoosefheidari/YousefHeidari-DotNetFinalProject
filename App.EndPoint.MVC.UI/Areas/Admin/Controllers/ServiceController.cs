using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        public ServiceController(IServiceAppService serviceAppService, ICategoryAppService categoryAppService)
        {
            _serviceAppService = serviceAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(cancellationToken);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(services);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            var result = await _serviceAppService.Add(serviceDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _serviceAppService.Get(id, cancellationToken);
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceAppService.Update(serviceDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _serviceAppService.Get(id, cancellationToken);
            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            await _serviceAppService.Delete(categoryDTO.Id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
