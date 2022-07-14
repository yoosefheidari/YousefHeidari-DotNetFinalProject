using App.Domain.Core.BaseData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Contracts.Repositories
{
    public interface IStatusCommandRepository
    {
        Task<int> Add(StatusDTO status, CancellationToken cancellationToken);
        Task Update(StatusDTO status, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
