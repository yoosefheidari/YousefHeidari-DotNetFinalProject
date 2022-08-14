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
    public class StatusService : IStatusService
    {
        private readonly IStatusCommandRepository _statusCommandRepository;
        private readonly IStatusQueryRepository _statusQueryRepository;
        public StatusService(IStatusQueryRepository statusQueryRepository, IStatusCommandRepository statusCommandRepository)
        {
            _statusQueryRepository = statusQueryRepository;
            _statusCommandRepository = statusCommandRepository;
        }
        public async Task<int> Add(StatusDTO statusDTO, CancellationToken cancellationToken)
        {
            statusDTO.CreationDate = DateTimeOffset.Now;
            statusDTO.IsDeleted = false;
            var result = await _statusCommandRepository.Add(statusDTO, cancellationToken);
            return result;

        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _statusCommandRepository.Delete(id, cancellationToken);
        }

        public async Task EnsureStatusIsNotExist(string name, CancellationToken cancellationToken)
        {
            var service = await _statusQueryRepository.Get(name, cancellationToken);
            if (!(service == null))
                throw new Exception("وضعیت مورد نظر قبلا ایجاد شده است");
        }

        public async Task<StatusDTO> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _statusQueryRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _statusQueryRepository.GetAll(cancellationToken);
            return statuses;
        }

        public async Task Update(StatusDTO statusDTO, CancellationToken cancellationToken)
        {
            await _statusCommandRepository.Update(statusDTO, cancellationToken);
        }
    }
}
