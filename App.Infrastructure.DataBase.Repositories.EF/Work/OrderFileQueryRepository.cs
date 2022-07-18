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
    public class OrderFileQueryRepository : IOrderFileQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderFileQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        public async Task<OrderFile> Get(int id, CancellationToken cancellationToken)
        {
            var orderFile = await _appDbContext.OrderFiles
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return orderFile;
        }

        public async Task<List<OrderFile>> GetAll(CancellationToken cancellationToken)
        {
            var orderFiles = await _appDbContext.OrderFiles.ToListAsync(cancellationToken);
            return orderFiles;
        }

    }
}
