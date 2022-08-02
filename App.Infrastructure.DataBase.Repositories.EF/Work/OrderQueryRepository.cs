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
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    Address = x.Customer.Address,
                    Suggests = x.ExpertSuggests.Select(x => new SuggestDTO()
                    {
                        Id = x.Id,
                        CreationDate = x.CreationDate,
                        Description = x.Description,
                        IsDeleted = x.IsDeleted,
                        ExpertId = x.ExpertId,
                        IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                        OrderId = x.OrderId,
                        SuggestedPrice = x.SuggestedPrice,
                        ExpertName=x.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(x => new CommentDTO()
                    {
                        CreationDate = x.CreationDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsApproved = x.IsApproved,
                        IsDeleted = x.IsDeleted,
                        OrderId = x.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = x.Title,

                    }).ToList(),
                })
                .SingleAsync(cancellationToken);
            //var orderDto = new OrderDTO()
            //{
            //    Id = id,
            //    Description = order.Description,
            //    IsDeleted = order.IsDeleted,
            //    FinalPrice = order.FinalPrice,
            //    ConfirmedExpertId = order.ConfirmedExpertId,
            //    CreationDate = order.CreationDate,
            //    CustomerId = order.CustomerId,
            //    IsConfirmedByCustomer = order.IsConfirmedByCustomer,
            //    StatusId = order.StatusId,
            //    ServiceId = order.ServiceId,
            //    StatusName=order.Status.Name,

            //};
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
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    Address = x.Customer.Address,
                    Suggests = x.ExpertSuggests.Select(x => new SuggestDTO()
                    {
                        Id = x.Id,
                        CreationDate = x.CreationDate,
                        Description = x.Description,
                        IsDeleted = x.IsDeleted,
                        ExpertId = x.ExpertId,
                        IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                        OrderId = x.OrderId,
                        SuggestedPrice = x.SuggestedPrice,
                    }).ToList(),
                    Comments = x.Comments.Select(x => new CommentDTO()
                    {
                        CreationDate = x.CreationDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsApproved = x.IsApproved,
                        IsDeleted = x.IsDeleted,
                        OrderId = x.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = x.Title,

                    }).ToList(),
                })
                .ToListAsync(cancellationToken);

            return orders;
        }

        public async Task<List<OrderDTO>> GetAllExpertOrders(UserDTO expert, string query, CancellationToken cancellationToken)
        {
            //var orderss =await _appDbContext.Orders.Include(s => s.Service).ToListAsync();
            //var result= orderss.Where(x => expert.expertCategories.Any(d => d.Id == x.Service.CategoryId) && x.StatusId == 1 && x.IsConfirmedByCustomer == false).ToList();

            IQueryable<Order> queri = _appDbContext.Orders;
            if (query == "newest")
            {
                var orderss = await _appDbContext.Orders.Include(x => x.ExpertSuggests).Include(x => x.Customer).Include(s => s.Service).Include(x => x.Comments).Include(x => x.ExpertSuggests).Include(x => x.Status).ToListAsync();
                var result = orderss.Where(x => expert.expertCategories.Any(d => d.Id == x.Service.CategoryId) && x.StatusId == 1 && x.IsConfirmedByCustomer == false).ToList();
                return result.Select(x => new OrderDTO()
                {
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    StatusId = x.StatusId,
                    CreationDate = x.CreationDate,
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    Id = x.Id,
                    Description = x.Description,
                    CustomerName = x.Customer.UserName,
                    ServiceName = x.Service.Title,
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    Suggests = x.ExpertSuggests.Select(x => new SuggestDTO()
                    {
                        Id = x.Id,
                        CreationDate=x.CreationDate,
                        Description=x.Description,
                        IsDeleted=x.IsDeleted,
                        ExpertId=x.ExpertId,
                        IsConfirmedByCustomer=x.IsConfirmedByCustomer,
                        OrderId=x.OrderId,
                        SuggestedPrice=x.SuggestedPrice,
                    }).ToList(),
                    Comments = x.Comments.Select(f => new CommentDTO() { Id = f.Id, Description = f.Description, Title = f.Title, IsApproved = f.IsApproved, CreationDate = f.CreationDate, OrderId = f.OrderId }).ToList(),
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),

                }).ToList(); ;
                //queri = queri.Where(x => expert.expertCategories.Any(d => d.Id == x.Service.CategoryId) && x.StatusId==1 && x.IsConfirmedByCustomer == false);

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
                IsDeleted = x.IsDeleted,
                Id = x.Id,
                Description = x.Description,
                CustomerName = x.Customer.UserName,
                ServiceName = x.Service.Title,
                StatusName = x.Status.Name,
                StatusValue = x.Status.StatusValue,
                Comments = x.Comments.Select(f => new CommentDTO() { Id = f.Id, Description = f.Description, Title = f.Title, IsApproved = f.IsApproved, CreationDate = f.CreationDate, OrderId = f.OrderId }).ToList(),
                ShamsiCreationDate = x.CreationDate.ToShamsi(),

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
