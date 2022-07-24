

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

        public IViewComponentResult Invoke(CancellationToken cancellationToken)
        {
            var categoies =_categoryAppService.GetAllWithServices(cancellationToken).Result;
            return View("_CategoryList", categoies);
        }
    }
}
