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
    public class ServiceCommandRepository : IServiceCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        

        public async Task<int> Add(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            var service = new Service()
            {
                CreationDate = serviceDTO.CreationDate,
                ShortDescription = serviceDTO.ShortDescription,
                CategoryId = serviceDTO.CategoryId,
                Price = serviceDTO.Price,
                Title = serviceDTO.Title,
                
            };
            await _appDbContext.Services.AddAsync(service, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return service.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var tag = await _appDbContext.Services.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Services.Remove(tag);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        

        public async Task Update(ServiceDTO serviceDTO, CancellationToken cancellationToken)
        {
            var service = await _appDbContext.Services.SingleAsync(x => x.Id == serviceDTO.Id, cancellationToken);
            service.Title = serviceDTO.Title;
            service.ShortDescription = serviceDTO.ShortDescription;
            service.CreationDate = serviceDTO.CreationDate;
            service.CategoryId = serviceDTO.CategoryId;
            service.Price = serviceDTO.Price;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
