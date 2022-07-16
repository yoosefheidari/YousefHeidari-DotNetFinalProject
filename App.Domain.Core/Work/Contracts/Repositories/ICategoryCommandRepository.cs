using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ICategoryCommandRepository
    {
        Task<int> Add(CategoryDTO category,CancellationToken cancellationToken);
        Task Update(CategoryDTO category, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
