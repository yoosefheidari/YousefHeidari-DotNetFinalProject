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
    public class ExpertSkillQueryRepository : IExpertSkillQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertSkillQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ExpertCategory> Get(int id, CancellationToken cancellationToken)
        {
            var expertSkill = await _appDbContext.ExpertSkills
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return expertSkill;
        }

        public async Task<List<ExpertCategory>> GetAll(CancellationToken cancellationToken)
        {
            var expertSkills = await _appDbContext.ExpertSkills.ToListAsync(cancellationToken);
            return expertSkills;
        }
    }
}
