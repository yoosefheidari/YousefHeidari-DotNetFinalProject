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
    public class TagCommandRepository : ITagCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public TagCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(TagDTO tag, CancellationToken cancellationToken)
        {
            var newTag = new Tag()
            {
                Name = tag.Name,
                CreationDate = tag.CreationDate,
                HasValue = tag.HasValue,
                IsDeleted = tag.IsDeleted,
                TagGroupId = tag.TagGroupId
            };
            await _appDbContext.Tags.AddAsync(newTag, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newTag.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var tag = await _appDbContext.Tags.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Tags.Remove(tag);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(TagDTO tag, CancellationToken cancellationToken)
        {
            var tag1 = await _appDbContext.Tags.SingleAsync(x => x.Id == tag.Id, cancellationToken);
            tag1.TagGroupId = tag.TagGroupId;
            tag1.Name = tag.Name;
            tag1.IsDeleted = tag.IsDeleted;
            tag1.HasValue = tag.HasValue;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
