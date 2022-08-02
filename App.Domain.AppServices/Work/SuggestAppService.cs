using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class SuggestAppService : ISuggestAppService
    {
        private readonly ISuggestService _suggestService;
        public SuggestAppService(ISuggestService suggestService)
        {
            _suggestService = suggestService;
        }

        public async Task<int> CreateSuggest(int orderId, int expertId, int price, string description, CancellationToken cancellationToken)
        {
            SuggestDTO suggest = new()
            {
                SuggestedPrice = price,
                CreationDate = DateTime.Now,
                Description = description,
                ExpertId = expertId,
                IsConfirmedByCustomer = false,
                IsDeleted = false,
                OrderId = orderId,
            };
            var result = await _suggestService.Add(suggest, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _suggestService.Delete(id, cancellationToken);
        }

        public async Task<SuggestDTO> Get(int id, CancellationToken cancellationToken)
        {
            var suggest = await _suggestService.Get(id, cancellationToken);
            return suggest;
        }

        public async Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken)
        {
            var suggests = await _suggestService.GetAll(cancellationToken);
            return suggests;
        }

        public async Task<List<SuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var suggests = await _suggestService.GetAll(OrderId, cancellationToken);
            return suggests;
        }

        public async Task Update(SuggestDTO suggest, CancellationToken cancellationToken)
        {
            await _suggestService.Update(suggest, cancellationToken);
        }
    }
}
