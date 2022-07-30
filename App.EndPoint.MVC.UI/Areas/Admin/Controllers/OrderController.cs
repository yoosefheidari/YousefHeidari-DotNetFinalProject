using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IStatusAppServcie _statusAppServcie;
        public OrderController(IOrderAppService orderAppService, IStatusAppServcie statusAppServcie)
        {
            _orderAppService = orderAppService;
            _statusAppServcie = statusAppServcie;
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderAppService.GetAll(id, cancellationToken);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetail(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            return View(order);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var statuses = await _statusAppServcie.GetAll(cancellationToken);
            ViewBag.Statuses = statuses.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var order = await _orderAppService.Get(id, cancellationToken);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            await _orderAppService.Update(orderDTO, cancellationToken);
            return RedirectToAction("OrderDetail", new {id=orderDTO.Id});
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            await _orderAppService.Delete(orderDTO.Id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Files(int id, CancellationToken cancellationToken)
        {
            var files = await _orderAppService.GetAllFiles(id, cancellationToken);
            return View(files);
        }
        public async Task<IActionResult> DeleteFile(int id, CancellationToken cancellationToken)
        {
            await _orderAppService.DeleteOrderFile(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }


    }
}
