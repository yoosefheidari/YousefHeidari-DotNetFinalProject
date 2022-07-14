using App.Domain.Core.BaseData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Contracts.Repositories
{
    public interface IOpinionQueryRepository
    {
        Task<List<OpinionDTO>> GetAll(CancellationToken cancellationToken);
        Task<OpinionDTO> Get(int id, CancellationToken cancellationToken);
        Task<OpinionDTO> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
