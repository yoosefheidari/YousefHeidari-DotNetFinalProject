﻿using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        private readonly IFileService _fileService;
        private readonly ILogger<ServcieAppServcie> _logger;

        public ServcieAppServcie(IServiceService serviceService, IFileService fileService, ILogger<ServcieAppServcie> logger)
        {
            _serviceService = serviceService;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<int> Add(ServiceDTO serviceDTO, List<IFormFile> files, CancellationToken cancellationToken)
        {
            serviceDTO.Id = 0;
            await _serviceService.EnsureServiceIsNotExist(serviceDTO.Title, cancellationToken);
            serviceDTO.CreationDate = DateTimeOffset.Now;
            var serviceId = await _serviceService.Add(serviceDTO, cancellationToken);
            var fileIds = await _fileService.UploadFileAsync(files, cancellationToken);
            var result = await _serviceService.AddServiceFiles(serviceId, fileIds, cancellationToken);
            if (serviceId != 0)
            {
                _logger.LogInformation("new service {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new service failed", "add");
            }
            return serviceId;
        }

        public async Task AddServiceFile(int id, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var fileIds = await _fileService.UploadFileAsync(files, cancellationToken);
            var result = await _serviceService.AddServiceFiles(id, fileIds, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceService.Delete(id, cancellationToken);
            _logger.LogInformation("service with id {id} {action} successfully", id, "Delete");
        }

        public async Task DeleteServiceFile(int id, CancellationToken cancellationToken)
        {
            var result = await _fileService.Get(id, cancellationToken);
            var physicalFilePath = result.Path;
            await _fileService.DeletePhysicalFile(physicalFilePath, cancellationToken);
            await _serviceService.DeleteServiceFile(id, cancellationToken);
        }

        public async Task<ServiceDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _serviceService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<ServiceDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var servcies = await _serviceService.GetAll(id, cancellationToken);
            return servcies;
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var paths = await _serviceService.GetAllFiles(ServiceId, cancellationToken);
            return paths;
        }

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceService.Update(serviceDTO, cancellationToken);
        }
    }
}
