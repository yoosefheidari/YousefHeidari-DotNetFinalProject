using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IStatusQueryRepository
    {
        Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken);
        Task<StatusDTO> Get(int id, CancellationToken cancellationToken);
        Task<StatusDTO> Get(string name, CancellationToken cancellationToken);
    }
}
