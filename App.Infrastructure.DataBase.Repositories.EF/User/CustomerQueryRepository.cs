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
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CustomerDTO> Get(int id, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var customerDto = new CustomerDTO()
            {
                Id = id,
                Name = customer.Name,
                NationalCode = customer.NationalCode,
                Address = customer.Address,
                Family = customer.Family,
                IdentityUserId = customer.IdentityUserId,
                Mobile=customer.Mobile
            };
            return customerDto;
        }

        public async Task<List<CustomerDTO>> GetAll(CancellationToken cancellationToken)
        {
            var customers = await _appDbContext.Customers
                .Select(x => new CustomerDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NationalCode = x.NationalCode,
                    Address = x.Address,
                    Family = x.Family,
                    IdentityUserId = x.IdentityUserId,
                    Mobile = x.Mobile
                })
                .ToListAsync(cancellationToken);
            return customers;
        }

        public async Task<CustomerDTO> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers
                .Where(x => x.IdentityUserId == userId).SingleAsync(cancellationToken);
            var customerDto = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                NationalCode = customer.NationalCode,
                Address = customer.Address,
                Family = customer.Family,
                IdentityUserId = customer.IdentityUserId,
                Mobile = customer.Mobile
            };
            return customerDto;
        }
    }
}
