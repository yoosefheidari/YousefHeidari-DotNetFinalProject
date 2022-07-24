using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryDTO>> GetAllWithServices(CancellationToken cancellationToken);
        Task<int> Add(CategoryDTO category, CancellationToken cancellationToken);
        Task<CategoryDTO> Get(int id, CancellationToken cancellationToken);
        Task Update(CategoryDTO category, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
