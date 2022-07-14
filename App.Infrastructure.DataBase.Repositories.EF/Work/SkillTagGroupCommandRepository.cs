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
    public class SkillTagGroupCommandRepository : ISkillTagGroupCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillTagGroupCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(SkillTagGroup skillTagGroup, CancellationToken cancellationToken)
        {
            await _appDbContext.SkillTagGroups.AddAsync(skillTagGroup, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return skillTagGroup.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var skillTagGroup = await _appDbContext.SkillTagGroups.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.SkillTagGroups.Remove(skillTagGroup);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
