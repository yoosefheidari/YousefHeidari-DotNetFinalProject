using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class OrderCommandRepository : IOrderCommandRepository

    {
        private readonly AppDbContext _appDbContext;

        public OrderCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(OrderDTO order, CancellationToken cancellationToken)
        {
            var newOrder = new Order()
            {
                FinalizedDate = order.FinalizedDate,
                SkillId = order.SkillId,
                StatusId = order.StatusId,
                SuggestedWorkTimeByExpert = order.SuggestedWorkTimeByExpert,
                ConfirmedExpertId = order.ConfirmedExpertId,
                CustomerId = order.CustomerId,
                Description = order.Description,
                FinalPrice = order.FinalPrice,
                IsConfirmedByCustomer = order.IsConfirmedByCustomer,
                RequestedDate = order.RequestedDate,
                CreationDate = order.CreationDate,
            };
            await _appDbContext.Orders.AddAsync(newOrder, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newOrder.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OrderDTO order, CancellationToken cancellationToken)
        {
            var order1 = await _appDbContext.Orders.SingleAsync(x => x.Id == order.Id, cancellationToken);
            order1.ConfirmedExpertId=order.ConfirmedExpertId;
            order1.CustomerId=order.CustomerId;
            order1.Description=order.Description;
            order1.FinalPrice=order.FinalPrice;
            order1.ConfirmedExpertId = order.ConfirmedExpertId;
            order1.FinalizedDate = order.FinalizedDate;
            order1.StatusId = order.StatusId;
            order1.SkillId = order.SkillId;
            order1.RequestedDate = order.RequestedDate;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
