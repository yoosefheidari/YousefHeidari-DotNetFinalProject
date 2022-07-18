using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IAdminCommandRepository
    {
        Task<int> Add(UserDTO admin);
        Task Update(UserDTO admin);
        Task Delete(int id);
    }
}
