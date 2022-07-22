using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }


        public async Task<IActionResult> Create (List<IFormFile> files, CancellationToken cancellationToken)
        {
            
            return View();
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            
            return View();
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CommentDTO commentDTO, CancellationToken cancellationToken)
        {
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CommentDTO commentDTO, CancellationToken cancellationToken)
        {
            
            return RedirectToAction(nameof(Index));
        }
    }
}
