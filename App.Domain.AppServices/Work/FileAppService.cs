using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class FileAppService : IFileAppService
    {
        public Task<int> Add(List<IFormFile> files, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
