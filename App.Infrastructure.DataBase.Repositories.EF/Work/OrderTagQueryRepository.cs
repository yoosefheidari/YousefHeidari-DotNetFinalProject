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
    public class OrderTagQueryRepository : IOrderTagQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderTagQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderTag> Get(int id, CancellationToken cancellationToken)
        {
            var orderTag = await _appDbContext.OrderTags
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return orderTag;
        }

        public async Task<List<OrderTag>> GetAll(CancellationToken cancellationToken)
        {
            var orderTags = await _appDbContext.OrderTags.ToListAsync(cancellationToken);
            return orderTags;
        }
    }
}
