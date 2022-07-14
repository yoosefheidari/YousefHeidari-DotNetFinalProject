using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IIdentityUserQueryRepository
    {
        Task<List<IdentityUser<int>>> GetAll();
        Task<IdentityUser<int>> Get(int id);
        Task<IdentityUser<int>> Get(string name);
    }
}
