
using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryViewComponent(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var categoies = await _categoryAppService.GetAllWithServices(cancellationToken);
            return View("_CategoryList", categoies);
        }
    }
}
