using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.AppServices
{
    public interface ICommentAppService
    {
        Task<int> Add(CommentDTO comment, CancellationToken cancellationToken);
        Task Update(CommentDTO comment, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<CommentDTO>> GetAll(int approve, CancellationToken cancellationToken);
        Task<List<CommentDTO>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken);
        Task<CommentDTO> Get(int id, CancellationToken cancellationToken);
        Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
