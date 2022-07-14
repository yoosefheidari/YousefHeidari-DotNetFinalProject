using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.BaseData.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.BaseData
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CategoryDTO> Get(int id, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var categoryDto = new CategoryDTO()
            {
                Id = id,
                Name = category.Name,
                CreationDate = category.CreationDate,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                IsDeleted = category.IsDeleted,
                ParentCategoryId = category.ParentCategoryId,
            };
            return categoryDto;
        }

        public async Task<CategoryDTO> Get(string name, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories
                .Where(x => x.Name.ToLower() == name.ToLower()).SingleAsync(cancellationToken);
            var categoryDto = new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                CreationDate = DateTime.Now,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                IsDeleted = category.IsDeleted,
                ParentCategoryId = category.ParentCategoryId,
            };
            return categoryDto;
        }

        public async Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _appDbContext.Categories
                .Select(x => new CategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    DisplayOrder = x.DisplayOrder,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    ParentCategoryId = x.ParentCategoryId
                })
                .ToListAsync(cancellationToken);
            return categories;
        }
    }
}


