using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IIdentityUserCommandRepository
    {
        Task<int> Add(IdentityUser<int> user);
        Task Update(IdentityUser<int> user);
        Task Delete(int id);
    }
}
