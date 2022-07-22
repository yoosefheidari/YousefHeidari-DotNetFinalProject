using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
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

        public FileService(IFileCommandRepository fileCommandRepository, IFileQueryRepository fileQueryRepository)
        {
            _fileCommandRepository = fileCommandRepository;
            _fileQueryRepository = fileQueryRepository;
        }

        public Task<int> Add(List<IFormFile> files, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public Task<List<PhysicalFileDTO>> GetAll(int id,CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
