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
    public class SkillTagGroupQueryRepository : ISkillTagGroupQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillTagGroupQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CategoryTagGroup> Get(int id, CancellationToken cancellationToken)
        {
            var skillTagGroup = await _appDbContext.SkillTagGroups
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return skillTagGroup;
        }

        public async Task<List<CategoryTagGroup>> GetAll(CancellationToken cancellationToken)
        {
            var skillTagGroups = await _appDbContext.SkillTagGroups.ToListAsync(cancellationToken);
            return skillTagGroups;
        }
    }
}
