using App.Domain.Core.Work.Contracts.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class FileDeleteService : IFileDeleteService
    {
        private readonly IConfiguration _configuration;

        public FileDeleteService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken)
        {
            var rootpath = _configuration.GetSection("UploadPath").Value;
            File.Delete(Path.Combine(rootpath, fileName));
        }
    }
}
