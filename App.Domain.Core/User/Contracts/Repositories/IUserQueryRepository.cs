using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
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
        Task<UserDTO> GetUserByUserName(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<List<RoleDTO>> GetRoles();
    }
}
