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
    public class OrderFileCommandRepository : IOrderFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderFileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        public async Task<int> Add(OrderFileDTO orderFile, CancellationToken cancellationToken)
        {
            var newOrderFile = new OrderFile()
            {
                OrderId = orderFile.OrderId,
                FileId = orderFile.FileId,
                IsDeleted = orderFile.IsDeleted,
            };
            await _appDbContext.OrderFiles.AddAsync(newOrderFile, cancellationToken);
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
