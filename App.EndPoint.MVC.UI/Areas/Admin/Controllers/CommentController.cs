using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        public async Task<IActionResult> Index(int id,CancellationToken cancellationToken)
        {
            var comments=await _commentAppService.GetAll(id, cancellationToken);
            return View(comments);
        }

        public async Task<IActionResult> GetAllOrderComments(int id, CancellationToken cancellationToken)
        {
            var comments = await _commentAppService.GetAllOrderComments(id, cancellationToken);
            return View("OrderComments",comments);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var comment =await _commentAppService.Get(id, cancellationToken);
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CommentDTO commentDTO, CancellationToken cancellationToken)
        {
            await _commentAppService.Update(commentDTO, cancellationToken);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> EditOrderComments(int id, CancellationToken cancellationToken)
        {
            var comment = await _commentAppService.Get(id, cancellationToken);
            
            return View("EditOrderComment", comment);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrderComments(CommentDTO commentDTO, CancellationToken cancellationToken)
        {
            await _commentAppService.Update(commentDTO, cancellationToken);
            return RedirectToAction("GetAllOrderComments", new {id= commentDTO.OrderId.ToString() });
        }


        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var comment = await _commentAppService.Get(id, cancellationToken);
            return View(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CommentDTO commentDTO, CancellationToken cancellationToken)
        {
            await _commentAppService.Delete(commentDTO.Id, cancellationToken);            
            return RedirectToAction("GetAllOrderComments", new { id = commentDTO.OrderId.ToString() });
        }
    }
}
