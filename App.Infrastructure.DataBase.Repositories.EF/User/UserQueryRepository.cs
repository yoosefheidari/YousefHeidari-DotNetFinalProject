using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using App.Infrastructure.DataBase.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public UserQueryRepository(UserManager<AppUser> userManager, AppDbContext appDbContext, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
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

        public async Task<UserDTO> Get(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
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
            return userDto;
        }

        public async Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
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

        public Task<UserDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var expertCategories = await _appDbContext.ExpertCategories.Where(r => r.ExpertId == user.Id)
                .Select(x => x.Category)
                .Select(c => new CategoryDTO()
                {
                    CreationDate = c.CreationDate,
                    Id = c.Id,
                    IsDeleted = c.IsDeleted,
                    Name = c.Name,
                }).ToListAsync();

            var userDto = new UserDTO()
            {
                Id = user.Id,
                UserName = username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                expertCategories = expertCategories,
            };
            return userDto;
        }
    }
}
