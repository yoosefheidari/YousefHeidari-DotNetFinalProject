using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
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
    public class TagQueryRepository : ITagQueryRepository

    {
        private readonly AppDbContext _appDbContext;

        public TagQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TagDTO> Get(int id, CancellationToken cancellationToken)
        {
            var tag = await _appDbContext.Tags
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var tagDto = new TagDTO()
            {
                Id = id,
                Name = tag.Name,
                HasValue = tag.HasValue,
                CreationDate = tag.CreationDate,
                IsDeleted = tag.IsDeleted,
                TagGroupId = tag.TagGroupId
            };
            return tagDto;
        }

        public async Task<TagDTO> Get(string name, CancellationToken cancellationToken)
        {
            var tag = await _appDbContext.Tags
                .Where(x => x.Name == name).SingleAsync(cancellationToken);
            var tagDto = new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name,
                HasValue = tag.HasValue,
                CreationDate = tag.CreationDate,
                IsDeleted = tag.IsDeleted,
                TagGroupId = tag.TagGroupId
            };
            return tagDto;
        }

        public async Task<List<TagDTO>> GetAll(CancellationToken cancellationToken)
        {
            var tags = await _appDbContext.Tags
                .Select(x => new TagDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted,
                    HasValue = x.HasValue,
                    TagGroupId= x.TagGroupId
                })
                .ToListAsync(cancellationToken);
            return tags;
        }
    }
}
