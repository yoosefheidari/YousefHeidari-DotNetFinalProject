using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.BaseData.DTOs;
using App.Domain.Core.BaseData.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.BaseData
{
    public class OpinionCommandRepository : IOpinionCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public OpinionCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(OpinionDTO opinion, CancellationToken cancellationToken)
        {
            var newOpinion = new Opinion()
            {
                Description = opinion.Description,
                DoesRecomended = opinion.DoesRecomended,
                OrderId = opinion.OrderId,
                Rating = opinion.Rating,
                Title = opinion.Title,
                CreationDate = opinion.CreationDate,
            };
            await _appDbContext.Opinions.AddAsync(newOpinion, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newOpinion.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var opinion = await _appDbContext.Opinions.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Opinions.Remove(opinion);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OpinionDTO opinion, CancellationToken cancellationToken)
        {
            var opinion1 = await _appDbContext.Opinions.SingleAsync(x => x.Id == opinion.Id, cancellationToken);
            opinion1.Title = opinion.Title;
            opinion1.Description = opinion.Description;
            opinion1.DoesRecomended = opinion.DoesRecomended;
            opinion1.Rating = opinion.Rating;
            opinion1.OrderId = opinion.OrderId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
