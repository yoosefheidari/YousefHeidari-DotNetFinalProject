using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.BaseData.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.BaseData
{
    public class OpinionQueryRepository : IOpinionQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public OpinionQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OpinionDTO> Get(int id, CancellationToken cancellationToken)
        {
            var opinion = await _appDbContext.Opinions
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var opinionDto = new OpinionDTO()
            {
                Id = id,
                Description = opinion.Description,
                DoesRecomended = opinion.DoesRecomended,
                Rating = opinion.Rating,
                Title = opinion.Title,
                CreationDate = opinion.CreationDate,
            };
            return opinionDto;
        }

        public async Task<OpinionDTO> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var opinion = await _appDbContext.Opinions
                .Where(x => x.OrderId == orderId).SingleOrDefaultAsync(cancellationToken);
            var opinionDto = new OpinionDTO()
            {
                Id = opinion.Id,
                Description = opinion.Description,
                DoesRecomended = opinion.DoesRecomended,
                Rating = opinion.Rating,
                Title = opinion.Title,
                CreationDate = opinion.CreationDate,
            };
            return opinionDto;
        }

        public async Task<List<OpinionDTO>> GetAll(CancellationToken cancellationToken)
        {
            var opinions = await _appDbContext.Opinions
                .Select(x => new OpinionDTO()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    CreationDate = x.CreationDate,
                    Description = x.Description,
                    DoesRecomended= x.DoesRecomended,
                    Rating= x.Rating,
                    Title= x.Title,
                })
                .ToListAsync(cancellationToken);
            return opinions;
        }
    }
}
