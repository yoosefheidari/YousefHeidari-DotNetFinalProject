using App.Domain.Core.Work.Contracts.AppServices;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Work
{
    public class StatusAppService : IStatusAppServcie
    {
        private readonly IStatusService _statusService;
        private readonly ILogger<StatusAppService> _logger;

        public StatusAppService(IStatusService statusService, ILogger<StatusAppService> logger)
        {
            _statusService = statusService;
            _logger = logger;
        }

        public async Task<int> Add(StatusDTO status, CancellationToken cancellationToken)
        {
            await _statusService.EnsureStatusIsNotExist(status.Name, cancellationToken);
            var result = await _statusService.Add(status, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new status {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new status failed", "add");
            }
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _statusService.Delete(id, cancellationToken);
            _logger.LogInformation("status with id {id} {action} successfully", id, "Delete");
        }

        public Task<StatusDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = _statusService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _statusService.GetAll(cancellationToken);
            return statuses;
        }

        public async Task Update(StatusDTO category, CancellationToken cancellationToken)
        {
            await _statusService.Update(category, cancellationToken);
        }
    }
}
