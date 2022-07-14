using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IExpertQueryRepository
    {
        Task<List<ExpertDTO>> GetAll(CancellationToken cancellationToken);
        Task<ExpertDTO> Get(int id, CancellationToken cancellationToken);
        Task<ExpertDTO> GetByUserId(int userId, CancellationToken cancellationToken);
    }
}
