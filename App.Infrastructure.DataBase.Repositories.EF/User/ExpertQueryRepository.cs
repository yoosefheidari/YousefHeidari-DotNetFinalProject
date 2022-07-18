using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.User
{
    public class ExpertQueryRepository : IExpertQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserFileDTO> Get(int id, CancellationToken cancellationToken)
        {
            var expert = await _appDbContext.Experts
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var expertDto = new UserFileDTO()
            {
                Id = id,
                Name = expert.Name,
                NationalCode = expert.NationalCode,
                Address = expert.Address,
                Family = expert.Family,
                IdentityUserId = expert.IdentityUserId,
                Mobile = expert.Mobile,
                Email = expert.Email
            };
            return expertDto;
        }

        public async Task<List<UserFileDTO>> GetAll(CancellationToken cancellationToken)
        {
            var experts = await _appDbContext.Experts
                .Select(x => new UserFileDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NationalCode = x.NationalCode,
                    Address = x.Address,
                    Family = x.Family,
                    IdentityUserId = x.IdentityUserId,
                    Mobile = x.Mobile,
                    Email= x.Email
                    
                })
                .ToListAsync(cancellationToken);
            return experts;
        }

        public async Task<UserFileDTO> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var expert = await _appDbContext.Experts
                .Where(x => x.IdentityUserId == userId).SingleAsync(cancellationToken);
            var expertDto = new UserFileDTO()
            {
                Id = expert.Id,
                Name = expert.Name,
                NationalCode = expert.NationalCode,
                Address = expert.Address,
                Family = expert.Family,
                IdentityUserId = expert.IdentityUserId,
                Mobile = expert.Mobile,
                Email = expert.Email
            };
            return expertDto;
        }
    }
}
