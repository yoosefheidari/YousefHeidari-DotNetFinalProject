using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IUserCommandRepository
    {
        Task<int> Add(UserDTO user, string password, List<string>? roles);
        Task Update(AppUser user, List<string> roles, string password);
        Task Delete(int id);
        Task SignInUserById(int id,bool isPersistent);
        Task SignoutUser();
        Task<bool> LoginUser(string userName, string password, bool remember);
    }
}
