using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class CommentService : ICommentService
    {
        private readonly IServiceCommentCommandRepository _serviceCommentCommandRepository;
        private readonly IServiceCommentQueryRepository _serviceCommentQueryRepository;

        public CommentService(IServiceCommentQueryRepository serviceCommentQueryRepository, IServiceCommentCommandRepository serviceCommentCommandRepository)
        {
            _serviceCommentQueryRepository = serviceCommentQueryRepository;
            _serviceCommentCommandRepository = serviceCommentCommandRepository;
        }

        public async Task<int> Add(CommentDTO comment, CancellationToken cancellationToken)
        {
            comment.CreationDate=DateTimeOffset.Now;
            comment.IsDeleted = false;
            comment.IsApproved = null;
            var result = await _serviceCommentCommandRepository.Add(comment, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommentCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<CommentDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _serviceCommentQueryRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<CommentDTO>> GetAll(int approve, CancellationToken cancellationToken)
        {
            var comments= await _serviceCommentQueryRepository.GetAll(approve, cancellationToken);
            return comments;
        }

        public async Task<List<CommentDTO>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments=await _serviceCommentQueryRepository.GetAllOrderComments(OrderId, cancellationToken);
            return comments;
        }

        public async Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _serviceCommentQueryRepository.GetByOrderId(orderId, cancellationToken);
            return comment;
        }

        public async Task Update(CommentDTO comment, CancellationToken cancellationToken)
        {
            await _serviceCommentCommandRepository.Update(comment, cancellationToken);
        }
    }
}
