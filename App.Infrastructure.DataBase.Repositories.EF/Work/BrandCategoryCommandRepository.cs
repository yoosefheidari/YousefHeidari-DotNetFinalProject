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
    public class BrandCategoryCommandRepository : IBrandCategoryCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrandCategoryCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(BrandCategory brandCategory, CancellationToken cancellationToken)
        {
            await _appDbContext.BrandCategories.AddAsync(brandCategory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return brandCategory.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var brandCategory = await _appDbContext.BrandCategories.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.BrandCategories.Remove(brandCategory);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
