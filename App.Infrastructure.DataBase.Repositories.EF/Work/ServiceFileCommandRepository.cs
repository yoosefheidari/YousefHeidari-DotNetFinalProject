using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class ServiceFileCommandRepository : IServiceFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceFileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(ServiceFile serviceFile, CancellationToken cancellationToken)
        {
            await _appDbContext.ServiceFiles.AddAsync(serviceFile, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return serviceFile.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _appDbContext.ServiceFiles.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.ServiceFiles.Remove(serviceFile);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
