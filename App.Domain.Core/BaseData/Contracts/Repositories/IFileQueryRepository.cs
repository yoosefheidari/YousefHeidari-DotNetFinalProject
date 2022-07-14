using App.Domain.Core.BaseData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Contracts.Repositories
{
    public interface IFileQueryRepository
    {
        Task<List<PhysicalFileDTO>> GetAll(CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken);
    }
}
