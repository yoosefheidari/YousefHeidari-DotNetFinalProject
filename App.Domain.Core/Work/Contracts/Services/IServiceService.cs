using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAll(CancellationToken cancellationToken);
        Task<int> Add(ServiceDTO serviceDTO, CancellationToken cancellationToken);
        Task<ServiceDTO> Get(int id, CancellationToken cancellationToken);
        Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteServiceFile(int id, CancellationToken cancellationToken);
        Task<bool> AddServiceFiles(int ServiceId, List<int> fileIds, CancellationToken cancellationToken);
        Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);
    }
}
