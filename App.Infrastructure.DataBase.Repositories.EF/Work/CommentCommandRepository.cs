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
    public class CommentCommandRepository : ICommentCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(CommentDTO comment, CancellationToken cancellationToken)
        {
            var newComment = new Comment()
            {
                Description = comment.Description,
                DoesRecomended = comment.DoesRecomended,
                OrderId = comment.OrderId,
                Rating = comment.Rating,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
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
            var opinion1 = await _appDbContext.Comments.SingleAsync(x => x.Id == comment.Id, cancellationToken);
            opinion1.Title = comment.Title;
            opinion1.Description = comment.Description;
            opinion1.DoesRecomended = comment.DoesRecomended;
            opinion1.Rating = comment.Rating;
            opinion1.OrderId = comment.OrderId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
