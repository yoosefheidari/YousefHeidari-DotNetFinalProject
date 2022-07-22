using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StatusController : Controller
    {
        private readonly IStatusAppServcie _statusAppServcie;
        public StatusController(IStatusAppServcie statusAppServcie)
        {
            _statusAppServcie = statusAppServcie;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _statusAppServcie.GetAll(cancellationToken);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusDTO statusDTO, CancellationToken cancellationToken)
        {
            var result = await _statusAppServcie.Add(statusDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _statusAppServcie.Get(id, cancellationToken);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StatusDTO statusDTO, CancellationToken cancellationToken)
        {
            await _statusAppServcie.Update(statusDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _statusAppServcie.Get(id, cancellationToken);
            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(StatusDTO statusDTO, CancellationToken cancellationToken)
        {
            await _statusAppServcie.Delete(statusDTO.Id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
