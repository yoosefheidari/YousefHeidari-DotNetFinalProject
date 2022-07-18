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
        Task<List<AppUser>> GetAll();
        Task<AppUser> Get(int id);
        Task<AppUser> Get(string name);
    }
}
