using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface IStatusService
    {
        Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken);
        Task<int> Add(StatusDTO statusDTO, CancellationToken cancellationToken);
        Task<StatusDTO> Get(int id, CancellationToken cancellationToken);
        Task Update(StatusDTO statusDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task EnsureStatusIsNotExist(string name, CancellationToken cancellationToken);
    }
}
