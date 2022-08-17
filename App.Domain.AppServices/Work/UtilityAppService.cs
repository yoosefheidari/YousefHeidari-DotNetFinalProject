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
    public class UtilityAppService : IUtilityAppService
    {
        private readonly IUtilityService _utilityService;
        private readonly IDistributedCache _distributedCache;

        public UtilityAppService(IUtilityService utilityService, IDistributedCache distributedCache)
        {
            _utilityService = utilityService;
            _distributedCache = distributedCache;
        }

        public async Task<StatisticsDTO> GetLatestStatistics(CancellationToken cancellationToken)
        {
            var statistic = new StatisticsDTO();
            if (_distributedCache.Get("Statistics") != null)
            {
                var statisticBytes = _distributedCache.Get("Statistics");
                var statisticString = Encoding.UTF8.GetString(statisticBytes);
                statistic = JsonSerializer.Deserialize<StatisticsDTO>(statisticString);
            }
            else
            {
                statistic = await _utilityService.GetLatestStatistics(cancellationToken);
                var statisticString = JsonSerializer.Serialize(statistic);
                var statisticBytes = Encoding.UTF8.GetBytes(statisticString);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1200),
                };
                _distributedCache.Set("Statistics", statisticBytes, options);
            }
            return statistic;
        }
    }
}
