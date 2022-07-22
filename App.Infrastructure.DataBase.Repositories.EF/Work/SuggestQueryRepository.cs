using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class SuggestQueryRepository : ISuggestQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public SuggestQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SuggestDTO> Get(int id, CancellationToken cancellationToken)
        {
            var expertSuggest = await _appDbContext.Suggests
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var expertSuggestDto = new SuggestDTO()
            {
                Id = id,
                CreationDate = expertSuggest.CreationDate,
                ExpertId = expertSuggest.Id,
                IsConfirmedByCustomer = expertSuggest.IsConfirmedByCustomer,
                OrderId = expertSuggest.OrderId,
                SuggestedPrice = expertSuggest.SuggestedPrice,
                Description = expertSuggest.Description,
            };
            return expertSuggestDto;
        }

        public async Task<List<SuggestDTO>> GetAll(CancellationToken cancellationToken)
        {
            var expertSuggests = await _appDbContext.Suggests                
                .Select(x => new SuggestDTO()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ExpertId= x.ExpertId,
                    IsConfirmedByCustomer= x.IsConfirmedByCustomer,
                    OrderId= x.OrderId,
                    SuggestedPrice = x.SuggestedPrice
                })                
                .ToListAsync(cancellationToken);
            return expertSuggests;
        }

        public async Task<List<SuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var expertSuggests = await _appDbContext.Suggests
                .Where(x=>x.OrderId==OrderId)
                .Select(x => new SuggestDTO()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ExpertId = x.ExpertId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    OrderId = x.OrderId,
                    SuggestedPrice = x.SuggestedPrice,
                    Description=x.Description,
                    ExpertName=x.Expert.FirstName,
                    
                })
                .ToListAsync(cancellationToken);
            return expertSuggests;
        }
    }
}
