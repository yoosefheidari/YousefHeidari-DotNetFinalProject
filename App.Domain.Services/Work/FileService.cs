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

        public async Task<int> Add(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            return 2;

        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            var file = await _fileQueryRepository.Get(id, cancellationToken);
            file.Path = _configuration.GetSection("UploadPath").Value + @"\\" + file.Path;
            return file;
        }

        public Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var files = await _fileQueryRepository.GetAll(id, cancellationToken);
            return files;
        }

        public Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
