using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
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
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CategoryQueryRepository> _logger;

        public CategoryQueryRepository(AppDbContext appDbContext, ILogger<CategoryQueryRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<CategoryDTO> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _appDbContext.Categories
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
                var categoryDto = new CategoryDTO()
                {
                    Id = id,
                    Name = category.Name,
                    CreationDate = category.CreationDate,
                    IsDeleted = category.IsDeleted,
                };
                _logger.LogInformation($"دسته بندی با آی دی {id} فراخوانی شد", "id");
                return categoryDto;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("دسته بندی درخواست شده وجود ندارد");
            }
            
        }

        public async Task<CategoryDTO> Get(string name, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _appDbContext.Categories
                .Where(x => x.Name.ToLower() == name.ToLower()).SingleAsync(cancellationToken);
                var categoryDto = new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreationDate = DateTime.Now,
                    IsDeleted = category.IsDeleted,
                };
                _logger.LogInformation($"دسته بندی با نام {name} فراخوانی شد", "id");
                return categoryDto;
            }
            catch(Exception ex)
            {
                throw new KeyNotFoundException("دسته بندی درخواست شده وجود ندارد");
            }
            
        }

        public async Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _appDbContext.Categories
                .Select(x => new CategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted,
                })
                .ToListAsync(cancellationToken);
            return categories;
        }

        public async Task<List<CategoryDTO>> GetAllWithServices(CancellationToken cancellationToken)
        {
            var categories = await _appDbContext.Categories
                .Select(x => new CategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted,
                    Services = x.Services.Select(v => new ServiceDTO()
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Price = v.Price,
                        ShortDescription = v.ShortDescription,
                        CategoryId = v.CategoryId,
                        CreationDate = v.CreationDate,
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
            return categories;
        }
    }
}


