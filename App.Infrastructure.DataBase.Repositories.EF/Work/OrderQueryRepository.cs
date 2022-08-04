using App.Infrastructure.DataBase.SqlServer;
using App.Domain.Core.Work.Contracts.Repositories;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Work.DTOs;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.User.DTOs;
using App.Domain.Services.Utilities;

namespace App.Infrastructure.DataBase.Repositories.EF.Work
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderDTO> Get(int id, CancellationToken cancellationToken)
        {
            var orderDto = await _appDbContext.Orders
                .Where(x => x.Id == id)
                .Select(x => new OrderDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    StatusName = x.Status.Name,
                    CategoryId = x.Service.CategoryId,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    Address = x.Customer.Address,
                    Suggests = x.ExpertSuggests.Select(h => new SuggestDTO()
                    {
                        Id = h.Id,
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        IsDeleted = h.IsDeleted,
                        ExpertId = h.ExpertId,
                        IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                        OrderId = h.OrderId,
                        SuggestedPrice = h.SuggestedPrice,
                        ExpertName=h.Expert.UserName,                        
                    }).ToList(),
                    Comments = x.Comments.Select(h => new CommentDTO()
                    {
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        IsDeleted = h.IsDeleted,
                        OrderId = h.OrderId,
                        ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new PhysicalFileDTO()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        IsDeleted = z.IsDeleted,
                        Path = z.Path,
                    }).ToList(),

                })
                .SingleAsync(cancellationToken);
           
            return orderDto;
        }


        public async Task<List<OrderDTO>> GetAll(int id, CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _appDbContext.Orders;
            if (id == 1)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == false && x.StatusId == 1);
            }
            if (id == 2)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == true && (x.StatusId == 2 || x.StatusId == 3 || x.StatusId == 4));
            }
            if (id == 3)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == true && x.StatusId == 5);
            }
            var orders = await query
                .Select(x => new OrderDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    CategoryId = x.Service.CategoryId,
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    Address = x.Customer.Address,
                    Suggests = x.ExpertSuggests.Select(h => new SuggestDTO()
                    {
                        Id = h.Id,
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        IsDeleted = h.IsDeleted,
                        ExpertId = h.ExpertId,
                        IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                        OrderId = h.OrderId,
                        SuggestedPrice = h.SuggestedPrice,
                        ExpertName=h.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(h => new CommentDTO()
                    {
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        IsDeleted = h.IsDeleted,
                        OrderId = h.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                        ServiceId=x.ServiceId,
                        

                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new PhysicalFileDTO()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        IsDeleted = z.IsDeleted,
                        Path = z.Path,
                    }).ToList(),
                })
                .ToListAsync(cancellationToken);

            return orders;
        }
        
        public async Task<List<OrderDTO>> GetAllExpertOrders(UserDTO expert, string query, CancellationToken cancellationToken)
        {
            IQueryable<Order> queri = _appDbContext.Orders;

                //queri = queri.Where(x => expert.expertCategories.Any(d => d.Id == x.Service.CategoryId) && x.StatusId==1 && x.IsConfirmedByCustomer == false);
            if (query == "newest")
            {
                queri=queri.Where(x => expert.expertCategories.Select(x => x.Id).Contains(x.Service.CategoryId) && x.StatusId == 1 && x.IsConfirmedByCustomer == false);
            }
            else if (query == "suggest")
            {
                queri = queri.Where(x => x.ExpertSuggests.Any(d => d.ExpertId == expert.Id) && x.IsConfirmedByCustomer == false && (x.StatusId == 1));
            }
            else if (query == "current")
            {
                queri = queri.Where(x => x.ConfirmedExpertId == expert.Id && x.IsConfirmedByCustomer == true && (x.StatusId == 2 || x.StatusId == 3 || x.StatusId == 4));
            }
            else if (query == "finished")
            {
                queri = queri.Where(x => x.ConfirmedExpertId == expert.Id && x.IsConfirmedByCustomer == true && x.StatusId == 5);
            }
            else
            {
                return new List<OrderDTO>();
            }
            var orders = await queri.Select(x => new OrderDTO()
            {
                FinalPrice = x.FinalPrice,
                ConfirmedExpertId = x.ConfirmedExpertId,
                StatusId = x.StatusId,
                CreationDate = x.CreationDate,
                CustomerId = x.CustomerId,
                IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                ServiceId = x.ServiceId,
                Id = x.Id,
                CustomerName = x.Customer.UserName,
                ServiceName = x.Service.Title,
                StatusName = x.Status.Name,
                StatusValue = x.Status.StatusValue,ShamsiCreationDate = x.CreationDate.ToShamsi(), 
            }).ToListAsync(cancellationToken);
            return orders;
        }

        public async Task<List<PhysicalFileDTO>> GetAllFiles(int orderId, CancellationToken cancellationToken)
        {
            var files = await _appDbContext.OrderFiles.Where(x => x.OrderId == orderId).Select(x => x.File).Select(x => new PhysicalFileDTO()
            {
                Id = x.Id,
                Path = x.Path,
                CreationDate = x.CreationDate,
                IsDeleted = x.IsDeleted,
            }).ToListAsync(cancellationToken);
            return files;
        }
    }
}
