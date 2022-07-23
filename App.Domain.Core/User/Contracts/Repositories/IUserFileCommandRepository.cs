using App.Domain.Core.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IUserFileCommandRepository
    {
        Task<int> Add(UserFileDTO userFile, CancellationToken cancellationToken);
    }
}
