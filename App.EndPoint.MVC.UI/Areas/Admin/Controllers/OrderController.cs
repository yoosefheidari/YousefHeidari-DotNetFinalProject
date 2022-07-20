using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var comments = await _orderAppService.GetAll(cancellationToken);
            return View(comments);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var comment = await _orderAppService.Get(id, cancellationToken);
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            await _orderAppService.Update(orderDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var comment = await _orderAppService.Get(id, cancellationToken);
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            await _orderAppService.Delete(orderDTO.Id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
