using App.Domain.Core.Work.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.UI.Areas.Admin.ViewComponents
{
    public class NumberOfStatusesViewComponent : ViewComponent
    {
        private readonly IStatusAppServcie _statusAppServcie;

        public NumberOfStatusesViewComponent(IStatusAppServcie statusAppServcie)
        {
            _statusAppServcie = statusAppServcie;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var statuses = await _statusAppServcie.GetAll(cancellationToken);
            return View("_StatusesNumber", statuses.Count);

        }
    }
}
