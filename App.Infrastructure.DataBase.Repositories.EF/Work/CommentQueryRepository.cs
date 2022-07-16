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
    public class CommentQueryRepository : ICommentQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CommentDTO> Get(int id, CancellationToken cancellationToken)
        {
            var comment = await _appDbContext.Comments
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var opinionDto = new CommentDTO()
            {
                Id = id,
                Description = comment.Description,
                DoesRecomended = comment.DoesRecomended,
                Rating = comment.Rating,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
            };
            return opinionDto;
        }

        public async Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _appDbContext.Comments
                .Where(x => x.OrderId == orderId).SingleOrDefaultAsync(cancellationToken);
            var commentDto = new CommentDTO()
            {
                Id = comment.Id,
                Description = comment.Description,
                DoesRecomended = comment.DoesRecomended,
                Rating = comment.Rating,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
            };
            return commentDto;
        }

        public async Task<List<CommentDTO>> GetAll(CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.Comments
                .Select(x => new CommentDTO()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    CreationDate = x.CreationDate,
                    Description = x.Description,
                    DoesRecomended= x.DoesRecomended,
                    Rating= x.Rating,
                    Title= x.Title,
                })
                .ToListAsync(cancellationToken);
            return comments;
        }
    }
}
