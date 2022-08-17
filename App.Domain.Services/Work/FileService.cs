using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class FileService : IFileService
    {
        private readonly IFileQueryRepository _fileQueryRepository;
        private readonly IFileCommandRepository _fileCommandRepository;
        private readonly IOrderFileCommandRepository _orderFileCommandRepository;
        private readonly IConfiguration _configuration;

        public FileService(IFileCommandRepository fileCommandRepository,
            IFileQueryRepository fileQueryRepository,
            IOrderFileCommandRepository orderFileCommandRepository,
            IConfiguration configuration)
        {
            _fileCommandRepository = fileCommandRepository;
            _fileQueryRepository = fileQueryRepository;
            _orderFileCommandRepository = orderFileCommandRepository;
            _configuration = configuration;
        }

        public async Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken)
        {
            var rootpath =_configuration.GetSection("UploadPath").Value;
            File.Delete(Path.Combine(rootpath, fileName));
        }

        public async Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            var file = await _fileQueryRepository.Get(id, cancellationToken);
            return file;
        }

        public async Task<List<int>> UploadFileAsync(List<IFormFile> files, CancellationToken cancellationToken)
        {
            List<int> fileIds = new();
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var randomName = Guid.NewGuid().ToString();
                var uniqePath = randomName + "-" + fileName;
                var rootPath = _configuration.GetSection("UploadPath").Value;
                var fullfilePath = Path.Combine(rootPath, uniqePath);
                PhysicalFileDTO newFile = new()
                {
                    Path = uniqePath,
                    CreationDate = DateTimeOffset.Now,
                    IsDeleted = false,
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


