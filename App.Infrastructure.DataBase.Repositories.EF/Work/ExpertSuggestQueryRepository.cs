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
    public class ExpertSuggestQueryRepository : IExpertSuggestQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertSuggestQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ExpertSuggestDTO> Get(int id, CancellationToken cancellationToken)
        {
            var expertSuggest = await _appDbContext.ExpertSuggests
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var expertSuggestDto = new ExpertSuggestDTO()
            {
                Id = id,
                CreationDate = expertSuggest.CreationDate,
                ExpertId = expertSuggest.Id,
                IsConfirmedByCustomer = expertSuggest.IsConfirmedByCustomer,
                OrderId = expertSuggest.OrderId,
                Price = expertSuggest.Price
            };
            return expertSuggestDto;
        }

        public async Task<List<ExpertSuggestDTO>> GetAll(CancellationToken cancellationToken)
        {
            var expertSuggests = await _appDbContext.ExpertSuggests
                .Select(x => new ExpertSuggestDTO()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ExpertId= x.ExpertId,
                    IsConfirmedByCustomer= x.IsConfirmedByCustomer,
                    OrderId= x.OrderId,
                    Price= x.Price
                })
                .ToListAsync(cancellationToken);
            return expertSuggests;
        }

        public async Task<List<ExpertSuggestDTO>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var expertSuggests = await _appDbContext.ExpertSuggests
                .Where(x=>x.OrderId==OrderId)
                .Select(x => new ExpertSuggestDTO()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ExpertId = x.ExpertId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    OrderId = x.OrderId,
                    Price = x.Price
                })
                .ToListAsync(cancellationToken);
            return expertSuggests;
        }
    }
}
