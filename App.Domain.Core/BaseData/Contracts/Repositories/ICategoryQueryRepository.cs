using App.Domain.Core.BaseData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Contracts.Repositories
{
    public interface ICategoryQueryRepository
    {
        Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDTO> Get(int id, CancellationToken cancellationToken);
        Task<CategoryDTO> Get(string name, CancellationToken cancellationToken);
    }
}
