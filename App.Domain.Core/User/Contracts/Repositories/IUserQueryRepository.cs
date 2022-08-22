using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IUserQueryRepository
    {
        Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken);
        Task<UserDTO> Get(int id);
        Task<UserDTO> Get(string username);
        Task<UserDTO> GetUserByUserName(string username);
        Task<UserDTO>? GetUserByEmail(string email);
        Task<List<RoleDTO>> GetRoles();
        Task<List<CommentDTO>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken);
        Task<List<CategoryDTO>?> GetExpertSkills(string username, CancellationToken cancellationToken);
        Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken);
        Task<List<OrderDTO>?> GetUserOrders(string username, CancellationToken cancellationToken);
        Task<List<SuggestDTO>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken);
        Task<List<CommentDTO>?> GetOrderComments(int orderId, CancellationToken cancellationToken);
    }
}
