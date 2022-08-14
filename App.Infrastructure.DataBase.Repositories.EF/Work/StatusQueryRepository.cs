using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class StatusQueryRepository : IStatusQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public StatusQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StatusDTO> Get(int id, CancellationToken cancellationToken)
        {
            var status = await _appDbContext.Statuses
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var statusDto = new StatusDTO()
            {
                Id = id,
                IsDeleted = status.IsDeleted,
                Name = status.Name,
                CreationDate = status.CreationDate
            };
            return statusDto;
        }

        public async Task<StatusDTO> Get(string name, CancellationToken cancellationToken)
        {
            var status = await _appDbContext.Statuses
                .Where(x => x.Name == name).SingleOrDefaultAsync(cancellationToken);
            if (status == null)
                return null;
            var statusDto = new StatusDTO()
            {
                Id = status.Id,
                IsDeleted = status.IsDeleted,
                Name = status.Name,
                CreationDate = status.CreationDate
            };
            return statusDto;
        }

        public async Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _appDbContext.Statuses
                .Select(x => new StatusDTO()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    IsDeleted = x.IsDeleted,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
            return statuses;
        }
    }
}
