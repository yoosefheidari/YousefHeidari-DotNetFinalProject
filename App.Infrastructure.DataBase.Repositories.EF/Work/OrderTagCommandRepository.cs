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
    public class OrderTagCommandRepository : IOrderTagCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderTagCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(OrderTag orderTag, CancellationToken cancellationToken)
        {
            await _appDbContext.OrderTags.AddAsync(orderTag, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return orderTag.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var orderTag = await _appDbContext.OrderTags.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.OrderTags.Remove(orderTag);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
