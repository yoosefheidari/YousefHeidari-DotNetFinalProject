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
    public class ServiceFileQueryRepository : IServiceFileQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceFileQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ServiceFile> Get(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _appDbContext.ServiceFiles
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return serviceFile;
        }

        public async Task<List<ServiceFile>> GetAll(CancellationToken cancellationToken)
        {
            var serviceFile = await _appDbContext.ServiceFiles.ToListAsync(cancellationToken);
            return serviceFile;
        }
    }
}
