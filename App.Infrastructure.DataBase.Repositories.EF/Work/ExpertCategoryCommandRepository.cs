using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class ExpertCategoryCommandRepository : IExpertCategoryCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertCategoryCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(ExpertCategory expertCategory, CancellationToken cancellationToken)
        {
            await _appDbContext.ExpertCategories.AddAsync(expertCategory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return expertCategory.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var expertCategory = await _appDbContext.ExpertCategories.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.ExpertCategories.Remove(expertCategory);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
