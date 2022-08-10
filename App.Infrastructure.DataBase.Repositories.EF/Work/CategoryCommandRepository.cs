using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CategoryCommandRepository> _logger;

        public CategoryCommandRepository(AppDbContext appDbContext, ILogger<CategoryCommandRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<int> Add(CategoryDTO category, CancellationToken cancellationToken)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                CreationDate = category.CreationDate,
                IsDeleted = category.IsDeleted,
            };
            await _appDbContext.Categories.AddAsync(newCategory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            if(newCategory.Id != 0)
            {
                _logger.LogInformation("دسته بندی جدید با موفقیت {عملیات} شد", "ایجاد");
            }
            else
            {
                _logger.LogWarning("{عملیات} دسته بندی جدید با خطا مواجه شد", "ایجاد");
            }
            return newCategory.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories.SingleAsync(x => x.Id == id,cancellationToken);
            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("دسته بندی جدید با موفقیت {حذف} شد", "عملیات");
        }

        public async Task Update(CategoryDTO category, CancellationToken cancellationToken)
        {
            var categori = await _appDbContext.Categories.SingleAsync(x => x.Id == category.Id, cancellationToken);            
            categori.Name = category.Name;
            categori.IsDeleted = category.IsDeleted;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("دسته بندی جدید با موفقیت {به روز رسانی} شد", "عملیات");
        }
    }
}
