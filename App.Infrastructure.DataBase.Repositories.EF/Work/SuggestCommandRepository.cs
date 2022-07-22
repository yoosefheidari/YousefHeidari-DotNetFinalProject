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
    public class SuggestCommandRepository : ISuggestCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public SuggestCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(SuggestDTO expertSuggest, CancellationToken cancellationToken)
        {
            var newExpertSuggest = new Suggest()
            {
                ExpertId = expertSuggest.Id,
                IsConfirmedByCustomer = expertSuggest.IsConfirmedByCustomer,
                OrderId = expertSuggest.OrderId,
                SuggestedPrice = expertSuggest.SuggestedPrice,
                CreationDate = expertSuggest.CreationDate,
                Description = expertSuggest.Description,
            };
            await _appDbContext.Suggests.AddAsync(newExpertSuggest, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newExpertSuggest.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var expertSuggest = await _appDbContext.Suggests.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Suggests.Remove(expertSuggest);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(SuggestDTO expertSuggest, CancellationToken cancellationToken)
        {
            var expertSuggest1 = await _appDbContext.Suggests.SingleAsync(x => x.Id == expertSuggest.Id, cancellationToken);
            expertSuggest1.IsConfirmedByCustomer= expertSuggest.IsConfirmedByCustomer;
            expertSuggest1.OrderId = expertSuggest.OrderId;
            expertSuggest1.SuggestedPrice = expertSuggest.SuggestedPrice;
            expertSuggest1.ExpertId = expertSuggest.ExpertId;
            expertSuggest.Description = expertSuggest1.Description;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
