using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IServiceCommentQueryRepository
    {
        Task<List<CommentDTO>> GetAll(int approve, CancellationToken cancellationToken);
        Task<CommentDTO> Get(int id, CancellationToken cancellationToken);
        Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<CommentDTO>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken);
    }
}
