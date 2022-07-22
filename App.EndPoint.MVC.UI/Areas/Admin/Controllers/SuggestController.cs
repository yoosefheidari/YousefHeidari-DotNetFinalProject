using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuggestController : Controller
    {
        private readonly ISuggestAppService _suggestAppService;

        public SuggestController(ISuggestAppService suggestAppService)
        {
            _suggestAppService = suggestAppService;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            int orderId = id;
            var suggests = await _suggestAppService.GetAll(orderId, cancellationToken);
            return View(suggests);
        }


        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var suggest = await _suggestAppService.Get(id, cancellationToken);

            return View(suggest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SuggestDTO suggestDTO, CancellationToken cancellationToken)
        {
            await _suggestAppService.Update(suggestDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var suggest = await _suggestAppService.Get(id, cancellationToken);
            return View(suggest);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SuggestDTO suggestdto, CancellationToken cancellationToken)
        {
            await _suggestAppService.Delete(suggestdto.Id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
