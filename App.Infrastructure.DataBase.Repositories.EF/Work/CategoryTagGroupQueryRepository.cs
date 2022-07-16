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
    public class CategoryTagGroupQueryRepository : ICategoryTagGroupQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryTagGroupQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CategoryTagGroup> Get(int id, CancellationToken cancellationToken)
        {
            var categoryTagGroup = await _appDbContext.CategoryTagGroups
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return categoryTagGroup;
        }

        public async Task<List<CategoryTagGroup>> GetAll(CancellationToken cancellationToken)
        {
            var categoryTagGroup = await _appDbContext.CategoryTagGroups.ToListAsync(cancellationToken);
            return categoryTagGroup;
        }
    }
}
