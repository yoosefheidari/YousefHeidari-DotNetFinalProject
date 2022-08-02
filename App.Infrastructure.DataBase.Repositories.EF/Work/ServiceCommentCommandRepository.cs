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
    public class ServiceCommentCommandRepository : IServiceCommentCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceCommentCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(CommentDTO comment, CancellationToken cancellationToken)
        {
            var newComment = new ServiceComment()
            {
                Description = comment.Description,
                OrderId = comment.OrderId,
                Title = comment.Title,
                CreationDate = comment.CreationDate,                
                IsApproved = comment.IsApproved,
                IsDeleted = comment.IsDeleted,
            };
            await _appDbContext.Comments.AddAsync(newComment, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newComment.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var comment = await _appDbContext.Comments.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CommentDTO comment, CancellationToken cancellationToken)
        {
            var comment1 = await _appDbContext.Comments.SingleAsync(x => x.Id == comment.Id, cancellationToken);
            comment1.Title = comment.Title;
            comment1.Description = comment.Description;
            comment1.OrderId = comment.OrderId;
            //comment1.ServiceId = comment.ServiceId;
            comment1.IsApproved = comment.IsApproved;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
