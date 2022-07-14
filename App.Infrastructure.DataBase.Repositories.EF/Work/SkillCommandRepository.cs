using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class SkillCommandRepository : ISkillCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(SkillDTO skill, CancellationToken cancellationToken)
        {
            var newSkill = new Skill()
            {
                Name = skill.Name,
                CreationDate = skill.CreationDate,
                CategoryId = skill.CategoryId                
            };
            await _appDbContext.Skills.AddAsync(newSkill, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newSkill.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var skill = await _appDbContext.Skills.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Skills.Remove(skill);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(SkillDTO skill, CancellationToken cancellationToken)
        {
            var skill1 = await _appDbContext.Skills.SingleAsync(x => x.Id == skill.Id, cancellationToken);
            skill1.Name = skill.Name;
            skill1.CategoryId = skill.CategoryId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
