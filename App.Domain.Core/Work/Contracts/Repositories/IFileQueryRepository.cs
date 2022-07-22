using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IFileQueryRepository
    {
        Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken);
    }
}
