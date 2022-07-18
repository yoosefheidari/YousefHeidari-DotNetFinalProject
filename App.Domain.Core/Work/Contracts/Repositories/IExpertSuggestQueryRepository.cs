using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IExpertSuggestQueryRepository
    {
        Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken);
        Task<List<SuggestDTO>> GetAll (int OrderId, CancellationToken cancellationToken);
        Task<SuggestDTO> Get(int id, CancellationToken cancellationToken);
    }
}
