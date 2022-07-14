using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IIdentityRoleCommandRepository
    {
        Task<int> Add(IdentityRole<int> role);
        Task Update(IdentityRole<int> role);
        Task Delete(int id);
    }
}
