using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class FileQueryRepository : IFileQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public FileQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PhysicalFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            var file = await _appDbContext.Files
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var fileDto = new PhysicalFileDTO()
            {
                Id = id,
                IsDeleted = file.IsDeleted,
                CreationDate = file.CreationDate,
                Path = file.Path
            };
            return fileDto;
        }

        public async Task<PhysicalFileDTO> Get(string path, CancellationToken cancellationToken)
        {
            var file = await _appDbContext.Files
                .Where(x => x.Path.ToLower() == path.ToLower()).SingleAsync(cancellationToken);
            var fileDto = new PhysicalFileDTO()
            {
                Id = file.Id,
                IsDeleted = file.IsDeleted,
                CreationDate = file.CreationDate,
                Path = file.Path
            };
            return fileDto;
        }

        public async Task<List<PhysicalFileDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            var query = _appDbContext.OrderFiles.Include(x=>x.File).Include(x=>x.Order).ToList();
            var result=_appDbContext.Orders.Include(x=>x.OrderFiles).SelectMany(x=>x.OrderFiles).ToList();

            var files = await _appDbContext.Files
                .Select(x => new PhysicalFileDTO()
                {
                    Id = x.Id,
                    Path = x.Path,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted,
                })
                .ToListAsync(cancellationToken);
            return files;

            
        }
    }
}
