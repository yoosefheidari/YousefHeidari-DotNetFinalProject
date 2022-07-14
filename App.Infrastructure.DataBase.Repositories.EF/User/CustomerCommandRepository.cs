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
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Add(CustomerDTO customer, CancellationToken cancellationToken)
        {
            var newCustomer = new Customer()
            {
                Name = customer.Name,
                Family=customer.Family,
                Address=customer.Address,
                IdentityUserId=customer.IdentityUserId,
                Mobile=customer.Mobile,
                NationalCode=customer.NationalCode
            };
            await _appDbContext.Customers.AddAsync(newCustomer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return newCustomer.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers.SingleAsync(x => x.Id == id, cancellationToken);
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CustomerDTO customer, CancellationToken cancellationToken)
        {
            var customer1=await _appDbContext.Customers.SingleAsync(x=>x.Id==customer.Id, cancellationToken);
            customer1.Name = customer.Name;
            customer1.Family = customer.Family;
            customer1.Address = customer.Address;
            customer1.IdentityUserId = customer.IdentityUserId;
            customer1.Mobile = customer.Mobile;
            customer1.NationalCode = customer.NationalCode;
            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
