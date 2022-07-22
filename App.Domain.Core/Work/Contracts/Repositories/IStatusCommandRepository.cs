using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IStatusCommandRepository
    {
        Task<int> Add(StatusDTO status, CancellationToken cancellationToken);
        Task Update(StatusDTO status, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
