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
    public class ServiceCommentQueryRepository : IServiceCommentQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceCommentQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CommentDTO> Get(int id, CancellationToken cancellationToken)
        {
            var comment = await _appDbContext.Comments
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var commentDto = new CommentDTO()
            {
                Id = id,
                Description = comment.Description,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
                //ServiceId = comment.ServiceId,
                OrderId = comment.OrderId,
                IsDeleted = comment.IsDeleted,
                IsApproved = comment.IsApproved,
            };
            return commentDto;
        }

        public async Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _appDbContext.Comments
                .Where(x => x.OrderId == orderId).SingleOrDefaultAsync(cancellationToken);
            var commentDto = new CommentDTO()
            {
                Id = comment.Id,
                Description = comment.Description,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
                //ServiceId = comment.ServiceId,
                OrderId = comment.OrderId,
                IsDeleted = comment.IsDeleted,
                IsApproved = comment.IsApproved,
            };
            return commentDto;
        }

        public async Task<List<CommentDTO>> GetAll(int approve, CancellationToken cancellationToken)
        {
            IQueryable<ServiceComment> query = _appDbContext.Comments;
            if (approve == 1) { }
            if (approve == 2) { query = query.Where(x => x.IsApproved == true); }
            if (approve == 3) { query = query.Where(x => x.IsApproved == null); }
            if (approve == 4) { query = query.Where(x => x.IsApproved == false); }
            var comments = await query
                .Select(x => new CommentDTO()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    CreationDate = x.CreationDate,
                    Description = x.Description,
                    //ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    Title = x.Title,
                    IsApproved = x.IsApproved,
                })
                .ToListAsync(cancellationToken);
            return comments;
        }

        public async Task<List<CommentDTO>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.Orders.Where(x => x.Id == OrderId)
                .SelectMany(s => s.Comments)
                .Select(x => new CommentDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    OrderId = x.OrderId,
                    //ServiceId=x.ServiceId,
                    Description=x.Description,
                    CreationDate = x.CreationDate,
                    IsDeleted=x.IsDeleted,
                    IsApproved=x.IsApproved,
                })
                .ToListAsync(cancellationToken);
            return comments;
        }
    }
}
