using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Contracts.Repositories
{
    public interface IUtilityRepositoy
    {
        Task<StatisticsDTO> GetLatestStatistics(CancellationToken cancellationToken);
    }
}
