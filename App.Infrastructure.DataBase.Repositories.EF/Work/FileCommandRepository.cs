using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class FileCommandRepository : IFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public FileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            var newFile = new PhysicalFile()
            {
                CreationDate = file.CreationDate,
                ExpertId = file.ExpertId,
                IsDeleted = file.IsDeleted,
                OrderId = file.OrderId,
                Path = file.Path
            };
            await _appDbContext.Files.AddAsync(newFile, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newFile.Id;
        }

        public Task<int> Add(List<IFormFile> files, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var file = await _appDbContext.Files.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Files.Remove(file);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(PhysicalFileDTO file, CancellationToken cancellationToken)
        {
            var file1 = await _appDbContext.Files.SingleAsync(x => x.Id == file.Id, cancellationToken);
            file1.IsDeleted = file.IsDeleted;
            file1.OrderId = file.OrderId;
            file1.Path = file.Path;
            file1.ExpertId = file.ExpertId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
