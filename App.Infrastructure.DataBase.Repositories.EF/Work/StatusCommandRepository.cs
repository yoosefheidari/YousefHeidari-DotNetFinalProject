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
    public class StatusCommandRepository : IStatusCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public StatusCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(StatusDTO status, CancellationToken cancellationToken)
        {
            var newStatus = new Status()
            {
                IsDeleted = status.IsDeleted,
                Name = status.Name,
                CreationDate = status.CreationDate,
            };
            await _appDbContext.Statuses.AddAsync(newStatus, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newStatus.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var status = await _appDbContext.Statuses.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Statuses.Remove(status);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(StatusDTO status, CancellationToken cancellationToken)
        {
            var status1 = await _appDbContext.Statuses.SingleAsync(x => x.Id == status.Id, cancellationToken);
            status1.IsDeleted = status.IsDeleted;
            status1.Name = status.Name;
            status1.CreationDate = status.CreationDate;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
