﻿using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using App.Domain.Services.Utilities;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.User
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserQueryRepository> _logger;

        public UserQueryRepository(UserManager<AppUser> userManager, AppDbContext appDbContext, RoleManager<IdentityRole<int>> roleManager, ILogger<UserQueryRepository> logger)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            _logger = logger;
        }



        public async Task<UserDTO> Get(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            UserDTO userDto = new()
            {
                Id = user.Id,
                Address = user.Address,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                UserName = user.UserName
            };
            return userDto;
        }

        public async Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var fff = _appDbContext.Orders;
            var users = await _userManager.Users.Select(x => new UserDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Address = x.Address,
                Email = x.Email,
                LastName = x.LastName,
                Mobile = x.Mobile,
                NationalCode = x.NationalCode,
                PhoneNumber = x.PhoneNumber,
                ProfilePicture = x.ProfilePicture,
                UserName = x.UserName,
            }).ToListAsync(cancellationToken);

            foreach (var item in users)
            {
                var user = await _userManager.FindByIdAsync(item.Id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                item.Roles = roles.ToList();

                item.expertCategories =
                    await _appDbContext.ExpertCategories
                    .Where(x => x.ExpertId == item.Id)
                    .Select(x => x.Category)
                    .Select(x => new CategoryDTO()
                    {
                        CreationDate = x.CreationDate,
                        IsDeleted = x.IsDeleted,
                        Id = x.Id,
                        Name = x.Name
                    }).ToListAsync(cancellationToken);
            }
            if (string.IsNullOrWhiteSpace(search))
            {
                //s.ForEach(async x=>await _userManager.GetRolesAsync(x))
                //var userrole = await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == item.Id));
                if (id == 1)
                {
                    users = users.Where(x => x.Roles.Count == 1).ToList();
                    return users;
                }
                if (id == 2)
                {
                    users = users.Where(x => x.Roles.Count == 2).ToList();
                    return users;
                }
                return users;
            }
            else
            {
                users = users
                    .Where(x => x.UserName.Contains(search) ||
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.Email.Contains(search))
                    .ToList();
                return users;
            }

        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = roles.Select(x => new RoleDTO() { Id = x.Id, Name = x.Name, }).ToList();
            return roleDtos;
        }

        public async Task<UserDTO>? GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            else
            {
                UserDTO? userDto = new UserDTO()
                {
                    Address = user.Address,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    Id = user.Id,
                    LastName = user.LastName,
                    Mobile = user.Mobile,
                    NationalCode = user.NationalCode,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                };
                return userDto;
            }
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            UserDTO userDto = new()
            {
                Id = user.Id,
                Address = user.Address,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                UserName = user.UserName,

            };
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToList();
            userDto.expertCategories = await _appDbContext.ExpertCategories
                    .Where(x => x.ExpertId == userDto.Id)
                    .Select(x => x.Category)
                    .Select(x => new CategoryDTO()
                    {
                        CreationDate = x.CreationDate,
                        IsDeleted = x.IsDeleted,
                        Id = x.Id,
                        Name = x.Name
                    }).ToListAsync();
            return userDto;
        }
        public async Task<List<CategoryDTO>?> GetExpertSkills(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var skills = await _appDbContext.ExpertCategories
                        .Where(x => x.ExpertId == user.Id)
                        .Select(x => x.Category)
                        .Select(x => new CategoryDTO()
                        {
                            CreationDate = x.CreationDate,
                            IsDeleted = x.IsDeleted,
                            Id = x.Id,
                            Name = x.Name
                        }).ToListAsync();
                return skills;
            }
        }
        public async Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }
        }
        public async Task<List<OrderDTO>?> GetUserOrders(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                var orders = await _appDbContext.Orders.Where(x => x.CustomerId == user.Id).Select(x => new OrderDTO()
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
                }).ToListAsync();
                return orders;
            }
        }
        public async Task<List<SuggestDTO>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken)
        {
            var suggests = await _appDbContext.Suggests
                .Where(s => s.OrderId == orderId)
                .Select(h => new SuggestDTO()
                {
                    Id = h.Id,
                    CreationDate = h.CreationDate,
                    Description = h.Description,
                    IsDeleted = h.IsDeleted,
                    ExpertId = h.ExpertId,
                    IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                    OrderId = h.OrderId,
                    SuggestedPrice = h.SuggestedPrice,
                    ExpertName = h.Expert.UserName,
                }).ToListAsync(cancellationToken);
            if (suggests == null)
                return null;
            else
                return suggests;

        }
        public async Task<List<CommentDTO>?> GetOrderComments(int orderId, CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.Comments
                .Where(c => c.OrderId == orderId)
                .Select(h => new CommentDTO()
                {
                    CreationDate = h.CreationDate,
                    ShamsiCreationDate = h.CreationDate.ToShamsi(),
                    Description = h.Description,
                    Id = h.Id,
                    IsApproved = h.IsApproved,
                    IsDeleted = h.IsDeleted,
                    OrderId = h.OrderId,
                    //ServiceId = x.ServiceId,
                    Title = h.Title,
                    IsWriteByCustomer = h.IsWriteByCustomer
                }).ToListAsync(cancellationToken);
            if (comments == null)
                return null;
            else
                return comments;
        }


        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            else
            {
                UserDTO userDto = new()
                {
                    Id = user.Id,
                    Address = user.Address,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    Mobile = user.Mobile,
                    NationalCode = user.NationalCode,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture,
                    UserName = user.UserName,

                };
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToList();
                if (roles.Count == 1 && roles.Contains("Customer"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "مشتری");
                if (roles.Count == 2 && roles.Contains("Customer") && roles.Contains("Expert"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "متخصص");
                if (roles.Contains("Admin"))
                    _logger.LogInformation("کاربر مورد نظر {نقش} می باشد", "ادمین");

                userDto.expertCategories = await _appDbContext.ExpertCategories
                        .Where(x => x.ExpertId == userDto.Id)
                        .Select(x => x.Category)
                        .Select(x => new CategoryDTO()
                        {
                            CreationDate = x.CreationDate,
                            IsDeleted = x.IsDeleted,
                            Id = x.Id,
                            Name = x.Name
                        }).ToListAsync();

                userDto.UserOrders = await _appDbContext.Orders.Where(x => x.CustomerId == userDto.Id).Select(x => new OrderDTO()
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
                        ExpertName = h.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(h => new CommentDTO()
                    {
                        CreationDate = h.CreationDate,
                        ShamsiCreationDate = h.CreationDate.ToShamsi(),
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        IsDeleted = h.IsDeleted,
                        OrderId = h.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                        ServiceId = x.ServiceId,
                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new PhysicalFileDTO()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        IsDeleted = z.IsDeleted,
                        Path = z.Path,
                    }).ToList(),

                }).ToListAsync();
                return userDto;
            }


        }

        public async Task<List<CommentDTO>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken)
        {
            var comments = await _appDbContext.Comments
                .Where(c => c.Order.ConfirmedExpertId == expertId)
                .Select(v => new CommentDTO()
                {
                    Description = v.Description,
                    ShamsiCreationDate = v.CreationDate.ToShamsi(),
                    Id = v.Id,
                    IsApproved = v.IsApproved,
                    IsWriteByCustomer = v.IsWriteByCustomer,
                    OrderId = v.OrderId,
                    Title = v.Title,
                })
                .ToListAsync();
            return comments;
        }
    }
}
