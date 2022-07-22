using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileService _fileService;

        public FileAppService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<int> Add(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            return await _fileService.Add(file, cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var files=await _fileService.GetAll(id, cancellationToken);
            return files;
        }

        public Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
