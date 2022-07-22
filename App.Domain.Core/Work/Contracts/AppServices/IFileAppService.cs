using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.AppServices
{
    public interface IFileAppService
    {
        Task<int> Add(PhysicalFileDTO files, CancellationToken cancellationToken);
        Task Update(PhysicalFileDTO file, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken);
        Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken);
    }
}
