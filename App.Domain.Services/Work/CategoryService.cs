using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Work
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public CategoryService(ICategoryQueryRepository categoryQueryRepository, ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
            _categoryCommandRepository = categoryCommandRepository;
        }

        public async Task<List<CategoryDTO>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _categoryQueryRepository.GetAll(cancellationToken);
            return result;
        }
        public async Task<int> Add(CategoryDTO category, CancellationToken cancellationToken)
        {
            category.CreationDate = DateTimeOffset.Now;
            category.IsDeleted = false;
            var result = await _categoryCommandRepository.Add(category, cancellationToken);
            return result;
        }

        public async Task<CategoryDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryQueryRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task Update(CategoryDTO category, CancellationToken cancellationToken)
        {
            await _categoryCommandRepository.Update(category, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<List<CategoryDTO>> GetAllWithServices(CancellationToken cancellationToken)
        {
            var categories = await _categoryQueryRepository.GetAllWithServices(cancellationToken);
            return categories;
        }

        public async Task EnsureCategoryIsNotExist(string name, CancellationToken cancellationToken)
        {
            var category = await _categoryQueryRepository.Get(name, cancellationToken);
            if (!(category == null))
                throw new Exception("کتگوری مورد نظر قبلا ایجاد شده است");

        }
    }
}
