using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IServiceFileQueryRepository
    {
        Task<List<ServiceFile>> GetAll(CancellationToken cancellationToken);
        Task<ServiceFile> Get(int id, CancellationToken cancellationToken);
    }
}
