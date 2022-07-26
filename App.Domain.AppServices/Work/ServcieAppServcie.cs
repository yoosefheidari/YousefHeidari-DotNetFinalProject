using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Repositories;
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
    public class ServcieAppServcie : IServiceAppService
    {
        private readonly IServiceService _serviceService;
        private readonly IUploadService _uploadService;

        public ServcieAppServcie(IServiceService serviceService, IUploadService uploadService)
        {
            _serviceService = serviceService;
            _uploadService = uploadService;
        }

        public async Task<int> Add(ServiceDTO serviceDTO,List<IFormFile> files, CancellationToken cancellationToken)
        {
            serviceDTO.CreationDate=DateTimeOffset.Now;
            var serviceId=await _serviceService.Add(serviceDTO,cancellationToken);
            var fileIds = await _uploadService.UploadFileAsync(files, cancellationToken);
            var result=await _serviceService.AddServiceFiles(serviceId, fileIds, cancellationToken);
            return serviceId;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceService.Delete(id,cancellationToken);
        }

        public async Task<ServiceDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result=await _serviceService.Get(id,cancellationToken);
            return result;
        }

        public async Task<List<ServiceDTO>> GetAll(CancellationToken cancellationToken)
        {
            var servcies=await _serviceService.GetAll(cancellationToken);
            return servcies;
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var paths=await _serviceService.GetAllFiles(ServiceId, cancellationToken);
            return paths;
        }

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceService.Update(serviceDTO,cancellationToken);
        }
    }
}
