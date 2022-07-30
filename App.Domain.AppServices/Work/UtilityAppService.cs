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
    public class UtilityAppService : IUtilityAppService
    {
        private readonly IUtilityService _utilityService;

        public UtilityAppService(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        public async Task<StatisticsDTO> GetLatestStatistics(CancellationToken cancellationToken)
        {
            var statistics = await _utilityService.GetLatestStatistics(cancellationToken);
            return statistics;
        }
    }
}
