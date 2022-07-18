using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IAdminQueryRepository
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> Get(int id);
        Task<UserDTO> Get(string name);
    }
}
