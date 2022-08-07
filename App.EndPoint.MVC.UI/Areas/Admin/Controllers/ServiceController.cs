using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class ServiceController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        public ServiceController(IServiceAppService serviceAppService, ICategoryAppService categoryAppService)
        {
            _serviceAppService = serviceAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(int id,CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(id,cancellationToken);
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
        public async Task<IActionResult> Create(ServiceDTO serviceDTO, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var result = await _serviceAppService.Add(serviceDTO, files, cancellationToken);
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


        public async Task<IActionResult> Files(int id, CancellationToken cancellationToken)
        {
            var files = await _serviceAppService.GetAllFiles(id, cancellationToken);
            return View(files);
        }

        public async Task<IActionResult> DeleteFile(int id, CancellationToken cancellationToken)
        {
            await _serviceAppService.DeleteServiceFile(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddServiceFile(int id, CancellationToken cancellationToken)
        {
            
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> AddServiceFile(int id, List<IFormFile> files, CancellationToken cancellationToken)
        {
            await _serviceAppService.AddServiceFile(id, files, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
