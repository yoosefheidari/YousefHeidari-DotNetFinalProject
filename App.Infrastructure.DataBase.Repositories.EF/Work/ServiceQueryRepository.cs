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
    public class ServiceQueryRepository : IServiceQueryRepository

    {
        private readonly AppDbContext _appDbContext;

        public ServiceQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ServiceDTO> Get(int id, CancellationToken cancellationToken)
        {
            var service = await _appDbContext.Services
        .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var serviceDto = new ServiceDTO()
            {
                Id = id,
                CategoryId = service.CategoryId,
                ShortDescription = service.ShortDescription,
                CreationDate = service.CreationDate,
                Price = service.Price,
                Title = service.Title,
            };
            return serviceDto;
        }

        public async Task<ServiceDTO> Get(string name, CancellationToken cancellationToken)
        {
            var service = await _appDbContext.Services
        .Where(x => x.Title == name).SingleAsync(cancellationToken);
            var serviceDto = new ServiceDTO()
            {
                Id = service.Id,
                CategoryId = service.CategoryId,
                ShortDescription = service.ShortDescription,
                CreationDate = service.CreationDate,
                Price = service.Price,
                Title = service.Title,
            };
            return serviceDto;
        }

        public async Task<List<ServiceDTO>> GetAll(CancellationToken cancellationToken)
        {
            var services = await _appDbContext.Services
        .Select(x => new ServiceDTO()
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            ShortDescription = x.ShortDescription,
            CreationDate = x.CreationDate,
            Price = x.Price,
            Title = x.Title,
        })
        .ToListAsync(cancellationToken);
            return services;
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var files = await _appDbContext.ServiceFiles.Where(x => x.ServiceId == ServiceId).Select(x => x.File).Select(x=>new PhysicalFileDTO()
            {
                Id = x.Id,
                Path = x.Path,
                CreationDate= x.CreationDate,
                IsDeleted = x.IsDeleted,
            }).ToListAsync(cancellationToken);
            return files;
        }
    }
}