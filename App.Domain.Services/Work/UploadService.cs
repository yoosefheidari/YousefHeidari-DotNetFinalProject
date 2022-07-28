using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class UploadService : IUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileCommandRepository _fileCommandRepository;

        public UploadService(IConfiguration configuration, IFileCommandRepository fileCommandRepository)
        {
            _configuration = configuration;
            _fileCommandRepository = fileCommandRepository;
        }

        public async Task<List<int>> UploadFileAsync(List<IFormFile> files,CancellationToken cancellationToken)
        {
            List<int> fileIds = new();
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var randomName = Guid.NewGuid().ToString();
                var uniqePath = randomName+"-"+fileName;
                var rootPath = _configuration.GetSection("UploadPath").Value;
                var fullfilePath = Path.Combine(rootPath, uniqePath);
                PhysicalFileDTO newFile = new()
                {
                    Path = uniqePath
                };
                var id = await _fileCommandRepository.Add(newFile, cancellationToken);
                fileIds.Add(id);
                //var dest = System.IO.File.Create(fullfilePath);

                using (FileStream dest = new FileStream(fullfilePath, FileMode.Create))
                {
                    await file.CopyToAsync(dest, cancellationToken);
                }
                
                
                
            }
            return fileIds;
        }

    }
}
