using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.AppServices
{
    public interface ISuggestAppService
    {
        Task<int> Add(SuggestDTO suggest, CancellationToken cancellationToken);
        Task Update(SuggestDTO suggest, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken);
        Task<List<SuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken);
        Task<SuggestDTO> Get(int id, CancellationToken cancellationToken);
    }
}
