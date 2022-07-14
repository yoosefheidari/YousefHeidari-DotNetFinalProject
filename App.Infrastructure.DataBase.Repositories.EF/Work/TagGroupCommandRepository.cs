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
    public class TagGroupCommandRepository : ITagGroupCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public TagGroupCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(TagGroupDTO tagGroup, CancellationToken cancellationToken)
        {
            var newTagGroup = new TagGroup()
            {
                Name = tagGroup.Name,
                CreationDate = tagGroup.CreationDate,
                IsDeleted = tagGroup.IsDeleted,                
            };
            await _appDbContext.TagGroups.AddAsync(newTagGroup, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newTagGroup.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var tagGroup = await _appDbContext.TagGroups.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.TagGroups.Remove(tagGroup);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(TagGroupDTO tagGroup, CancellationToken cancellationToken)
        {
            var tagGroup1 = await _appDbContext.TagGroups.SingleAsync(x => x.Id == tagGroup.Id, cancellationToken);
            tagGroup1.IsDeleted = tagGroup.IsDeleted;
            tagGroup1.Name = tagGroup.Name;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
