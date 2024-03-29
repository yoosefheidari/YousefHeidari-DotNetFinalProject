﻿using App.Domain.Core.Work.Contracts.AppServices;
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

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var categoies = await _categoryAppService.GetAllWithServices(cancellationToken);
            return View("_CategoryList", categoies);

        }
    }
}
