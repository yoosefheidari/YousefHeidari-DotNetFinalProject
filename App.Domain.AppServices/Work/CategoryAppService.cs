using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        private readonly IDistributedCache _distributedCache;
        public CategoryAppService(ICategoryService categoryService, IDistributedCache distributedCache)
        {
            _categoryService = categoryService;
            _distributedCache = distributedCache;
        }

        public async Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAll(cancellationToken);
            return result;
        }

        public async Task<int> Add(CategoryDTO category, CancellationToken cancellationToken)
        {
            var result = await _categoryService.Add(category, cancellationToken);
            return result;
        }

        public async Task<CategoryDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.Get(id, cancellationToken);
            return result;
        }

        public async Task Update(CategoryDTO category, CancellationToken cancellationToken)
        {
            await _categoryService.Update(category, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Delete(id, cancellationToken);
        }

        public async Task<List<CategoryDTO>> GetAllWithServices(CancellationToken cancellationToken)
        {
            var fff = _distributedCache.Get("Categories");
            Console.WriteLine("hello");
            var categories = new List<CategoryDTO>();
            
            if (_distributedCache.Get("Categories") != null)
            {
                var categoryBytes = _distributedCache.Get("Categories");
                var categoryString = Encoding.UTF8.GetString(categoryBytes);
                categories = JsonSerializer.Deserialize<List<CategoryDTO>>(categoryString);
            }
            else
            {
                categories = await _categoryService.GetAllWithServices(cancellationToken);
                var categoryString=JsonSerializer.Serialize(categories);
                var categoryBytes=Encoding.UTF8.GetBytes(categoryString);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20),
                };
                _distributedCache.Set("Categories",categoryBytes,options);
            }
            return categories;
        }
    }
}
