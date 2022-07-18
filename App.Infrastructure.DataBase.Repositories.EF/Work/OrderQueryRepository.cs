using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var orderDto = new OrderDTO()
            {
                Id = id,
                Description = order.Description,
                IsDeleted = order.IsDeleted,
                FinalPrice = order.FinalPrice,
                ConfirmedExpertId = order.ConfirmedExpertId,
                CreationDate = order.CreationDate,
                CustomerId = order.CustomerId,
                IsConfirmedByCustomer = order.IsConfirmedByCustomer,
                StatusId = order.StatusId,
                ServiceId = order.ServiceId,
            };
            return orderDto;
        }


        public async Task<List<OrderDTO>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _appDbContext.Orders
                .Select(x => new OrderDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                })
                .ToListAsync(cancellationToken);
            return orders;
        }
    }
}
