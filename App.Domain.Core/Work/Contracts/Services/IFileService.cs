using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface IFileService
    {
        Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken);
        Task<List<int>> UploadFileAsync(List<IFormFile> files, CancellationToken cancellationToken);
        Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken);
    }
}
