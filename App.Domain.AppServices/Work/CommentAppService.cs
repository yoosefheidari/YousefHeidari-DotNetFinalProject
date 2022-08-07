using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;
        private readonly IUserAppService _userAppService;

        public CommentAppService(ICommentService commentService, IUserAppService userAppService)
        {
            _commentService = commentService;
            _userAppService = userAppService;
        }

        public async Task<int> Add(CommentDTO comment, CancellationToken cancellationToken)
        {
            var result = await _commentService.Add(comment, cancellationToken);
            return result;
        }

        public async Task<int> CreateOrderComment(int orderId, int serviceId, string title, string description, CancellationToken cancellationToken)
        {
            var currentUser = await _userAppService.GetCurrentUserFullInfo();

            CommentDTO comment = new()
            {
                CreationDate = DateTimeOffset.Now,
                Description = description,
                IsApproved = null,
                IsDeleted = false,
                OrderId = orderId,
                ServiceId = serviceId,
                Title = title,
                IsWriteByCustomer=false
            };
            if(currentUser.Roles.Count==1 && currentUser.Roles.Contains("Customer") || currentUser.Roles.Contains("CUSTOMER"))
                comment.IsWriteByCustomer = true;
            var id = await _commentService.Add(comment, cancellationToken);
            return id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _commentService.Delete(id, cancellationToken);
        }

        public async Task<CommentDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _commentService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<CommentDTO>> GetAll(int approve, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetAll(approve, cancellationToken);
            return comments;
        }

        public async Task<List<CommentDTO>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetAllOrderComments(OrderId, cancellationToken);
            return comments;
        }

        public async Task<CommentDTO> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _commentService.GetByOrderId(orderId, cancellationToken);
            return comment;
        }

        public async Task Update(CommentDTO comment, CancellationToken cancellationToken)
        {
            await _commentService.Update(comment, cancellationToken);
        }
    }
}
