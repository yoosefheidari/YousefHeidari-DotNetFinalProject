using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IOrderQueryRepository
    {
        Task<List<OrderDTO>> GetAll(CancellationToken cancellationToken);
        Task<OrderDTO> Get(int id, CancellationToken cancellationToken);
    }
}
