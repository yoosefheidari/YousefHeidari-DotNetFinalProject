using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
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
        private readonly IOrderFileCommandRepository _orderFileCommandRepository;

        private readonly IOrderFileQueryRepository _orderFileQueryRepository;
        private readonly IServiceFileCommandRepository _serviceFileCommandRepository;   
        private readonly IServiceFileQueryRepository _serviceFileQueryRepository;

        public FileService(IFileCommandRepository fileCommandRepository, IFileQueryRepository fileQueryRepository, IOrderFileCommandRepository orderFileCommandRepository, IOrderFileQueryRepository orderFileQueryRepository, IServiceFileCommandRepository serviceFileCommandRepository, IServiceFileQueryRepository serviceFileQueryRepository)
        {
            _fileCommandRepository = fileCommandRepository;
            _fileQueryRepository = fileQueryRepository;
            _orderFileCommandRepository = orderFileCommandRepository;
            _orderFileQueryRepository = orderFileQueryRepository;
            _serviceFileCommandRepository = serviceFileCommandRepository;
            _serviceFileQueryRepository = serviceFileQueryRepository;
        }

        public async Task<int> Add(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            var result= await _fileCommandRepository.Add(file, cancellationToken);
            OrderFile b = new() { FileId = result, OrderId = 2 };
            await _orderFileCommandRepository.Add(b, cancellationToken);
            return result;
               
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

        public async Task<List<PhysicalFileDTO>> GetAll(int id,CancellationToken cancellationToken)
        {
            var files=await _fileQueryRepository.GetAll(id, cancellationToken);
            return files;
        }

        public Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
