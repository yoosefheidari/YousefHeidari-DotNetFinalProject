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
        Task<List<UserFileDTO>> GetAll(CancellationToken cancellationToken);
        Task<UserFileDTO> Get(int id, CancellationToken cancellationToken);
        Task<UserFileDTO> GetByUserId(int userId, CancellationToken cancellationToken);
    }
}
