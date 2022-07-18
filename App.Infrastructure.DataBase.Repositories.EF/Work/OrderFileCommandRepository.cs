using App.Domain.Core.Work.Contracts.Repositories;
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
    public class OrderFileCommandRepository : IOrderFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderFileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        public async Task<int> Add(OrderFile orderFile, CancellationToken cancellationToken)
        {
            await _appDbContext.OrderFiles.AddAsync(orderFile, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return orderFile.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var orderFile = await _appDbContext.OrderFiles.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.OrderFiles.Remove(orderFile);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
