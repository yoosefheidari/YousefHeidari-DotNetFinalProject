using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IBrandQueryRepository
    {
        Task<List<BrandDTO>> GetAll(CancellationToken cancellationToken);
        Task<BrandDTO> Get(int id, CancellationToken cancellationToken);
        Task<BrandDTO> Get(string name, CancellationToken cancellationToken);
    }
}
