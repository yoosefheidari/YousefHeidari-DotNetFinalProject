using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class ExpertSuggestCommandRepository : IExpertSuggestCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertSuggestCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(ExpertSuggestDTO expertSuggest, CancellationToken cancellationToken)
        {
            var newExpertSuggest = new ExpertSuggest()
            {
                ExpertId = expertSuggest.Id,
                IsConfirmedByCustomer = expertSuggest.IsConfirmedByCustomer,
                OrderId = expertSuggest.OrderId,
                Price = expertSuggest.Price,
                CreationDate = expertSuggest.CreationDate,
            };
            await _appDbContext.ExpertSuggests.AddAsync(newExpertSuggest, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newExpertSuggest.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var expertSuggest = await _appDbContext.ExpertSuggests.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.ExpertSuggests.Remove(expertSuggest);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ExpertSuggestDTO expertSuggest, CancellationToken cancellationToken)
        {
            var expertSuggest1 = await _appDbContext.ExpertSuggests.SingleAsync(x => x.Id == expertSuggest.Id, cancellationToken);
            expertSuggest1.IsConfirmedByCustomer= expertSuggest.IsConfirmedByCustomer;
            expertSuggest1.OrderId = expertSuggest.OrderId;
            expertSuggest1.Price = expertSuggest.Price;
            expertSuggest1.ExpertId = expertSuggest.ExpertId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
