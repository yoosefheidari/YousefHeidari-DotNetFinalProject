using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IOrderFileQueryRepository
    {
        Task<List<OrderFile>> GetAll(CancellationToken cancellationToken);
        Task<OrderFile> Get(int id, CancellationToken cancellationToken);
    }
}
