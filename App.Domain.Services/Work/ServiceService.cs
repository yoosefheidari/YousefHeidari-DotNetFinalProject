using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public ServiceService(IServiceQueryRepository serviceQueryRepository, IServiceCommandRepository serviceCommandRepository, IServiceFileCommandRepository serviceFileCommandRepository, IConfiguration configuration)
        {
            _serviceQueryRepository = serviceQueryRepository;
            _serviceCommandRepository = serviceCommandRepository;
            _serviceFileCommandRepository = serviceFileCommandRepository;
            _configuration = configuration;
        }

        public async Task<int> Add(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            serviceDTO.CreationDate = DateTimeOffset.Now;
            var result = await _serviceCommandRepository.Add(serviceDTO, cancellationToken);
            return result;
        }

        public async Task<bool> AddServiceFiles(int ServiceId, List<int> fileIds, CancellationToken cancellationToken)
        {
            foreach (var fileId in fileIds)
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

        public async Task DeleteServiceFile(int id, CancellationToken cancellationToken)
        {
            await _serviceCommandRepository.DeleteServiceFile(id, cancellationToken);
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

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetSection("DownloadPath").Value;
            var paths = await _serviceQueryRepository.GetAllFiles(ServiceId, cancellationToken);
            foreach (var path in paths)
            {
                path.Path = rootPath+"/"+path.Path;
            }
            return paths;
        }

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceCommandRepository.Update(serviceDTO, cancellationToken);
        }
    }
}
