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
    public class SkillQueryRepository : ISkillQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DTO> Get(int id, CancellationToken cancellationToken)
        {
            var skill = await _appDbContext.Skills
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var skillDto = new DTO()
            {
                Id = id,
                Name = skill.Name,
                CreationDate = skill.CreationDate,
                CategoryId = skill.CategoryId
            };
            return skillDto;
        }

        public async Task<DTO> Get(string name, CancellationToken cancellationToken)
        {
            var skill = await _appDbContext.Skills
                .Where(x => x.Name == name).SingleAsync(cancellationToken);
            var skillDto = new DTO()
            {
                Id = skill.Id,
                Name = skill.Name,
                CreationDate = skill.CreationDate,
                CategoryId = skill.CategoryId
            };
            return skillDto;
        }

        public async Task<List<DTO>> GetAll(CancellationToken cancellationToken)
        {
            var skills = await _appDbContext.Skills
                .Select(x => new DTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    CategoryId= x.CategoryId
                })
                .ToListAsync(cancellationToken);
            return skills;
        }
    }
}
