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
    public class CategoryTagGroupCommandRepository : ICategoryTagGroupCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryTagGroupCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(CategoryTagGroup categoryTagGroup, CancellationToken cancellationToken)
        {
            await _appDbContext.CategoryTagGroups.AddAsync(categoryTagGroup, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return categoryTagGroup.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var categoryTagGroup = await _appDbContext.CategoryTagGroups.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.CategoryTagGroups.Remove(categoryTagGroup);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
