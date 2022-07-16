using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface ICommentQueryRepository
    {
        Task<List<CommentDTO>> GetAll(CancellationToken cancellationToken);
        Task<CommentDTO> Get(int id, CancellationToken cancellationToken);
        Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
