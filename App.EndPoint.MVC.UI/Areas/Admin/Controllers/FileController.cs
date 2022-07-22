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
        private readonly IConfiguration _configuration;

        public FileController(IFileAppService fileAppService, IConfiguration configuration)
        {
            _fileAppService = fileAppService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Create(int id, CancellationToken cancellationToken)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (ICollection<IFormFile> files, CancellationToken cancellationToken)
        {
            var result = _configuration.GetSection("UploadPath").Value;

            
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var path = Path.Combine(result, fileName);
                FileStream ffile = new FileStream(path, FileMode.Create);
                file.CopyTo(ffile);
                var data = new PhysicalFileDTO()
                {
                    CreationDate = DateTimeOffset.Now,

                    IsDeleted = false,

                    Path = fileName,
                };
                await _fileAppService.Add(data, cancellationToken);
            }

            return View();
        }
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            var files = await _fileAppService.GetAll(id, cancellationToken);
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
