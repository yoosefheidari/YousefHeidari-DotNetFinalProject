using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
            var result= await _categoryService.Get(id, cancellationToken);
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
            var categories = await _categoryService.GetAllWithServices(cancellationToken);
            return categories;
        }
    }
}
