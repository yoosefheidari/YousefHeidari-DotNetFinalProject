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
    public class CategoryCommandRepository : ICategoryCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(CategoryDTO category, CancellationToken cancellationToken)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                CreationDate = category.CreationDate,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                IsDeleted = category.IsDeleted,
                ParentCategoryId = category.ParentCategoryId,
            };
            await _appDbContext.Categories.AddAsync(newCategory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newCategory.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories.SingleAsync(x => x.Id == id,cancellationToken);
            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CategoryDTO category, CancellationToken cancellationToken)
        {
            var categori = await _appDbContext.Categories.SingleAsync(x => x.Id == category.Id, cancellationToken);
            categori.DisplayOrder = category.DisplayOrder;
            categori.IsActive = category.IsActive;
            categori.Name = category.Name;
            categori.ParentCategoryId = category.ParentCategoryId;
            categori.IsDeleted = category.IsDeleted;
            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
