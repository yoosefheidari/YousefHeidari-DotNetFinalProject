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
            var orderDto = await _appDbContext.Orders
                .Where(x => x.Id == id)
                .Select(d=>new OrderDTO()
                {
                    Id = id,
                    Description = d.Description,
                    IsDeleted = d.IsDeleted,
                    FinalPrice = d.FinalPrice,
                    ConfirmedExpertId = d.ConfirmedExpertId,
                    CreationDate = d.CreationDate,
                    CustomerId = d.CustomerId,
                    IsConfirmedByCustomer = d.IsConfirmedByCustomer,
                    StatusId = d.StatusId,
                    ServiceId = d.ServiceId,
                    StatusName = d.Status.Name,
                    StatusValue=d.Status.StatusValue,
                })
                .SingleAsync(cancellationToken);
            //var orderDto = new OrderDTO()
            //{
            //    Id = id,
            //    Description = order.Description,
            //    IsDeleted = order.IsDeleted,
            //    FinalPrice = order.FinalPrice,
            //    ConfirmedExpertId = order.ConfirmedExpertId,
            //    CreationDate = order.CreationDate,
            //    CustomerId = order.CustomerId,
            //    IsConfirmedByCustomer = order.IsConfirmedByCustomer,
            //    StatusId = order.StatusId,
            //    ServiceId = order.ServiceId,
            //    StatusName=order.Status.Name,
                
            //};
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
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                })
                .ToListAsync(cancellationToken);
            return orders;
        }
    }
}
