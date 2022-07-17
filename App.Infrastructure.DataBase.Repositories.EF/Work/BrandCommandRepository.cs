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
    public class BrandCommandRepository : IBrandCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrandCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(BrandDTO brandDTO, CancellationToken cancellationToken)
        {
            var newBrand = new Brand()
            {
                Name = brandDTO.Name,
                CreationDate = brandDTO.CreationDate,
                IsDeleted = false
            };
            await _appDbContext.Brands.AddAsync(newBrand, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newBrand.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var brand = await _appDbContext.Brands.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Brands.Remove(brand);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(BrandDTO brandDTO, CancellationToken cancellationToken)
        {
            var brand1 = await _appDbContext.Brands.SingleAsync(x => x.Id == brandDTO.Id, cancellationToken);
            brand1.Name = brandDTO.Name;
            brand1.IsDeleted = brandDTO.IsDeleted;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
