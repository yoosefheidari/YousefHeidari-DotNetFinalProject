using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class BrandCategoryQueryRepository : IBrandCategoryQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrandCategoryQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<BrandCategory> Get(int id, CancellationToken cancellationToken)
        {
            var brandCategory = await _appDbContext.BrandCategories
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return brandCategory;
        }

        public async Task<List<BrandCategory>> GetAll(CancellationToken cancellationToken)
        {
            var brandCategory = await _appDbContext.BrandCategories.ToListAsync(cancellationToken);
            return brandCategory;
        }
    }
}
