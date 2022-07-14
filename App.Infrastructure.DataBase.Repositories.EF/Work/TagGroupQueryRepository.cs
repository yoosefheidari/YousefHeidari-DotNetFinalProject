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
    public class TagGroupQueryRepository : ITagGroupQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public TagGroupQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TagGroupDTO> Get(int id, CancellationToken cancellationToken)
        {
            var tagGroup = await _appDbContext.TagGroups
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var tagGroupDto = new TagGroupDTO()
            {
                Id = id,
                Name = tagGroup.Name,
                CreationDate = tagGroup.CreationDate,
                IsDeleted = tagGroup.IsDeleted
            };
            return tagGroupDto;
        }

        public async Task<TagGroupDTO> Get(string name, CancellationToken cancellationToken)
        {
            var tagGroup = await _appDbContext.TagGroups
                .Where(x => x.Name == name).SingleAsync(cancellationToken);
            var tagGroupDto = new TagGroupDTO()
            {
                Id = tagGroup.Id,
                Name = tagGroup.Name,
                CreationDate = tagGroup.CreationDate,
                IsDeleted = tagGroup.IsDeleted
            };
            return tagGroupDto;
        }

        public async Task<List<TagGroupDTO>> GetAll(CancellationToken cancellationToken)
        {
            var tagGroups = await _appDbContext.TagGroups
                .Select(x => new TagGroupDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted                    
                })
                .ToListAsync(cancellationToken);
            return tagGroups;
        }
    }
}
