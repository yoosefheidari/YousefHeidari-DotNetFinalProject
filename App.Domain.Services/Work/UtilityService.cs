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
    public class UtilityService : IUtilityService
    {
        private readonly IUtilityRepositoy _utilityRepositoy;

        public UtilityService(IUtilityRepositoy utilityRepositoy)
        {
            _utilityRepositoy = utilityRepositoy;
        }

        public async Task<StatisticsDTO> GetLatestStatistics(CancellationToken cancellationToken)
        {
            var statistics=await _utilityRepositoy.GetLatestStatistics(cancellationToken);
            return statistics;
        }
    }
}
