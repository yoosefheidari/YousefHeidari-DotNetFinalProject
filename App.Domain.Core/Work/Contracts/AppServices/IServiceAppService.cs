using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.AppServices
{
    public interface IServiceAppService
    {
        Task<List<ServiceDTO>> GetAll(int id, CancellationToken cancellationToken);
        Task<int> Add(ServiceDTO serviceDTO, List<IFormFile> files, CancellationToken cancellationToken);
        Task<ServiceDTO> Get(int id, CancellationToken cancellationToken);
        Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteServiceFile(int id, CancellationToken cancellationToken);
        Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);
        Task AddServiceFile(int id, List<IFormFile> files, CancellationToken cancellationToken);
    }
}
