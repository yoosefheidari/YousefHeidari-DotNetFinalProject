using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.ViewComponents
{
    public class AdminCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryAppService _categoryAppService;

        public AdminCategoryViewComponent(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public IViewComponentResult Invoke(CancellationToken cancellationToken)
        {
            var categoies = _categoryAppService.GetAllWithServices(cancellationToken).Result;
            return View("_CategoryList", categoies);
            
        }
    }
}
