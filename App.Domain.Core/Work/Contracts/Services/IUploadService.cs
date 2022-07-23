using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Services
{
    public interface IUploadService
    {
        Task<List<int>> UploadFileAsync(List<IFormFile> files, CancellationToken cancellationToken);

    }
}
