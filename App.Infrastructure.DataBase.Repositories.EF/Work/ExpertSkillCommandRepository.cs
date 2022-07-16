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
    public class ExpertSkillCommandRepository : IExpertSkillCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertSkillCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(ExpertCategory expertSkill, CancellationToken cancellationToken)
        {
            await _appDbContext.ExpertSkills.AddAsync(expertSkill, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return expertSkill.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var expertSkill = await _appDbContext.ExpertSkills.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.ExpertSkills.Remove(expertSkill);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
