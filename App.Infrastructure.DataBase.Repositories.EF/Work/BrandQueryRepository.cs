using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class BrandQueryRepository : IBrandQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrandQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<BrandDTO> Get(int id, CancellationToken cancellationToken)
        {
            var brand = await _appDbContext.Brands
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var brandDto = new BrandDTO()
            {
                Id = id,
                Name = brand.Name,
                IsDeleted=brand.IsDeleted,
                CreationDate = brand.CreationDate
            };
            return brandDto;
        }

        public async Task<BrandDTO> Get(string name, CancellationToken cancellationToken)
        {
            var brand = await _appDbContext.Brands
                .Where(x => x.Name == name).SingleAsync(cancellationToken);
            var brandDto = new BrandDTO()
            {
                Id = brand.Id,
                Name = brand.Name,
                IsDeleted = brand.IsDeleted,
                CreationDate = brand.CreationDate
            };
            return brandDto;
        }

        public async Task<List<BrandDTO>> GetAll(CancellationToken cancellationToken)
        {
            var brands = await _appDbContext.Brands
                .Select(x => new BrandDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return brands;
        }
    }
}
