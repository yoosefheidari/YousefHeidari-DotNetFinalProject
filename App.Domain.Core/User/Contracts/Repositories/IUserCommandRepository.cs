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
        Task<int> Add(AppUser user,List<string> roles,string password);
        Task Update(AppUser user, List<string> roles, string password);
        Task Delete(int id);
    }
}
