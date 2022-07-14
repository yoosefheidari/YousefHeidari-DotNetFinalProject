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
        Task<List<ExpertSuggestDTO>> GetAll(CancellationToken cancellationToken);
        Task<List<ExpertSuggestDTO>> GetAll (int OrderId, CancellationToken cancellationToken);
        Task<ExpertSuggestDTO> Get(int id, CancellationToken cancellationToken);
    }
}
