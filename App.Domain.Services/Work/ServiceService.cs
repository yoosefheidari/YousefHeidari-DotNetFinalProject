using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceCommandRepository _serviceCommandRepository;
        private readonly IServiceQueryRepository _serviceQueryRepository;
        private readonly IServiceFileCommandRepository _serviceFileCommandRepository;

        public ServiceService(IServiceQueryRepository serviceQueryRepository, IServiceCommandRepository serviceCommandRepository, IServiceFileCommandRepository serviceFileCommandRepository)
        {
            _serviceQueryRepository = serviceQueryRepository;
            _serviceCommandRepository = serviceCommandRepository;
            _serviceFileCommandRepository = serviceFileCommandRepository;
        }

        public async Task<int> Add(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            serviceDTO.CreationDate = DateTimeOffset.Now;
            var result = await _serviceCommandRepository.Add(serviceDTO, cancellationToken);
            return result;
        }

        public async Task<bool> AddServiceFiles(int ServiceId, List<int> fileIds, CancellationToken cancellationToken)
        {
            foreach(var fileId in fileIds)
            {
                ServiceFileDTO serviceFile = new()
                {
                    FileId = fileId,
                    ServiceId = ServiceId,
                    IsDeleted = false,
                };
                var result = await _serviceFileCommandRepository.Add(serviceFile, cancellationToken);
            }
            return true;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<ServiceDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _serviceQueryRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<ServiceDTO>> GetAll(CancellationToken cancellationToken)
        {
            var servcies = await _serviceQueryRepository.GetAll(cancellationToken);
            return servcies;
        }

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceCommandRepository.Update(serviceDTO, cancellationToken);
        }
    }
}
