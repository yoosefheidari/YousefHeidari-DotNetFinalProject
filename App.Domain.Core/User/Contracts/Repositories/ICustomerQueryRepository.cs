using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface ICustomerQueryRepository
    {
        Task<List<CustomerDTO>> GetAll(CancellationToken cancellationToken);
        Task<CustomerDTO> Get(int id, CancellationToken cancellationToken);
        Task<CustomerDTO> GetByUserId(int userId, CancellationToken cancellationToken);
    }
}
