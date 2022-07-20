using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
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

        public ServcieAppServcie(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<int> Add(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            serviceDTO.CreationDate=DateTimeOffset.Now;
            var result=await _serviceService.Add(serviceDTO,cancellationToken);
            return result;
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

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceService.Update(serviceDTO,cancellationToken);
        }
    }
}
