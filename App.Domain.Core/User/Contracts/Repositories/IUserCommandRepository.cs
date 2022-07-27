using App.Domain.Core.User.DTOs;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IUserCommandRepository
    {
        Task<int> Add(UserDTO user, string password);
        Task Update(UserDTO user, string oldPassword, string newPassword);
        Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken);
        Task Delete(int id);
        Task SignInUserById(int id,bool isPersistent);
        Task<int> LoginUser(string userName, string password, bool remember);
        Task SignoutUser();
    }
}
