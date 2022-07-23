using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.Entities;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Work.DTOs;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class ServiceFileCommandRepository : IServiceFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceFileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(ServiceFileDTO serviceFile, CancellationToken cancellationToken)
        {
            ServiceFile file = new()
            {
                FileId = serviceFile.FileId,
                ServiceId = serviceFile.ServiceId,
                IsDeleted = serviceFile.IsDeleted,
            };
            await _appDbContext.ServiceFiles.AddAsync(file, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return file.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _appDbContext.ServiceFiles.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.ServiceFiles.Remove(serviceFile);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
