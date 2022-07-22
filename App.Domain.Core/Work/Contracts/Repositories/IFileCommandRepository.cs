using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IFileCommandRepository
    {
        Task<int> Add(List<IFormFile> files, CancellationToken cancellationToken);
        Task Update(PhysicalFileDTO file, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
