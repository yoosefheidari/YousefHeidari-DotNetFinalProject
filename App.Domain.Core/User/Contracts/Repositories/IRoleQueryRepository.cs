using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IRoleQueryRepository
    {
        Task<List<IdentityRole<int>>> GetAll();
        Task<IdentityRole<int>> Get(int id);
        Task<IdentityRole<int>> Get(string name);
    }
}
