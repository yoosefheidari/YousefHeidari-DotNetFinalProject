using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ICategoryQueryRepository
    {
        Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryDTO>> GetAllWithServices(CancellationToken cancellationToken);
        Task<CategoryDTO> Get(int id, CancellationToken cancellationToken);
        Task<CategoryDTO> Get(string name, CancellationToken cancellationToken);
    }
}
