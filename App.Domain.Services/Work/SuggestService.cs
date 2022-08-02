using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class SuggestService : ISuggestService
    {
        private readonly ISuggestCommandRepository _suggestCommandRepository;
        private readonly ISuggestQueryRepository _suggestQueryRepository;
        public SuggestService(ISuggestCommandRepository suggestCommandRepository, ISuggestQueryRepository suggestQueryRepository)
        {
            _suggestCommandRepository = suggestCommandRepository;
            _suggestQueryRepository = suggestQueryRepository;
        }
        public async Task<int> Add(SuggestDTO suggest, CancellationToken cancellationToken)
        {
            var result=await _suggestCommandRepository.Add(suggest,cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _suggestCommandRepository.Delete(id,cancellationToken);
        }

        public async Task<SuggestDTO> Get(int id, CancellationToken cancellationToken)
        {
            var suggest=await _suggestQueryRepository.Get(id,cancellationToken);
            return suggest;
        }

        public async Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken)
        {
            var suggests = await _suggestQueryRepository.GetAll(cancellationToken);
            return suggests;
        }

        public async Task<List<SuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var suggests = await _suggestQueryRepository.GetAll(OrderId, cancellationToken);
            return suggests;
        }

        public async Task Update(SuggestDTO suggest, CancellationToken cancellationToken)
        {
            await _suggestCommandRepository.Update(suggest,cancellationToken);
        }
    }
}
