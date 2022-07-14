using App.Domain.Core.Operator.Entities;
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
    public class ExpertCommandRepository : IExpertCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public ExpertCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(ExpertDTO expert, CancellationToken cancellationToken)
        {
            var newExpert = new Expert()
            {
                Name = expert.Name,
                Family = expert.Family,
                Address = expert.Address,
                IdentityUserId = expert.IdentityUserId,
                Mobile = expert.Mobile,
                NationalCode = expert.NationalCode,
                Email = expert.Email
            };
            await _appDbContext.Experts.AddAsync(newExpert, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newExpert.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var expert = await _appDbContext.Experts.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Experts.Remove(expert);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ExpertDTO expert, CancellationToken cancellationToken)
        {
            var expert1 = await _appDbContext.Experts.SingleAsync(x => x.Id == expert.Id, cancellationToken);
            expert1.Name = expert.Name;
            expert1.Family = expert.Family;
            expert1.Address = expert.Address;
            expert1.IdentityUserId = expert.IdentityUserId;
            expert1.Mobile = expert.Mobile;
            expert1.NationalCode = expert.NationalCode;
            expert1.Email = expert.Email;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
