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
        Task<List<AdminDTO>> GetAll();
        Task<AdminDTO> Get(int id);
        Task<AdminDTO> Get(string name);
    }
}
