using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.AppServices
{
    public interface IUserAppService
    {
        Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken);
        Task<UserDTO> Get(int id);
        Task<UserDTO> GetUserByUserName(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<int> RegisterUser(UserDTO user, string password, CancellationToken cancellationToken);
        Task Update(UserDTO user, string oldPassword, string newPassword);
        Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken);
        Task Delete(int id);
        Task SignInUserById(int id);
        Task SignoutUser();
        Task<int> LoginUser(string userName, string password, bool remember);
        Task<List<RoleDTO>> GetRoles();
    }
}
