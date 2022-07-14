using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface ICustomerCommandRepository
    {
        Task<int> Add(CustomerDTO customer, CancellationToken cancellationToken);
        Task Update(CustomerDTO customer, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
