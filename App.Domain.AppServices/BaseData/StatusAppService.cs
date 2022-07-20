using App.Domain.Core.BaseData.Contracts.AppServices;
using App.Domain.Core.BaseData.Contracts.Services;
using App.Domain.Core.BaseData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.BaseData
{
    public class StatusAppService : IStatusAppServcie
    {
        private readonly IStatusService _statusService;

        public StatusAppService(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<int> Add(StatusDTO category, CancellationToken cancellationToken)
        {
            var result=await _statusService.Add(category, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _statusService.Delete(id, cancellationToken);
        }

        public Task<StatusDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result=_statusService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken)
        {
            var statuses= await _statusService.GetAll(cancellationToken);
            return statuses;
        }

        public async Task Update(StatusDTO category, CancellationToken cancellationToken)
        {
            await _statusService.Update(category, cancellationToken);
        }
    }
}
