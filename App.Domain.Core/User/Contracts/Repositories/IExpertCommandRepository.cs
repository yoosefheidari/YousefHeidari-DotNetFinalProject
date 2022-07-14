using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IExpertCommandRepository
    {
        Task<int> Add(ExpertDTO expert, CancellationToken cancellationToken);
        Task Update(ExpertDTO expert, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
