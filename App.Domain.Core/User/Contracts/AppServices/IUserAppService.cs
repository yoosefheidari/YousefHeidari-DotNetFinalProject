using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
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
        Task<List<AppUser>> GetAll();
        Task<AppUser> Get(int id);
        Task<UserDTO> GetUserByUserName(string username);
        Task<AppUser> GetUserByEmail(string email);
        Task<int> RegisterUser(UserDTO user, string password, List<IFormFile> files, CancellationToken cancellationToken, List<string>? roles);
        Task Update(AppUser user, List<string> roles, string password);
        Task Delete(int id);
        Task SignInUserById(int id);
        Task SignoutUser();
        Task<bool> LoginUser(string userName, string password, bool remember);
    }
}
