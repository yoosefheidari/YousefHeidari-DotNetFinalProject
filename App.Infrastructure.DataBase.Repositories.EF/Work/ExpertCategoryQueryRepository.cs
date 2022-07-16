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
    public class ExpertCategoryQueryRepository : IExpertCategoryQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertCategoryQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ExpertCategory> Get(int id, CancellationToken cancellationToken)
        {
            var expertCategory = await _appDbContext.ExpertCategories
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return expertCategory;
        }

        public async Task<List<ExpertCategory>> GetAll(CancellationToken cancellationToken)
        {
            var expertCategorys = await _appDbContext.ExpertCategories.ToListAsync(cancellationToken);
            return expertCategorys;
        }
    }
}
