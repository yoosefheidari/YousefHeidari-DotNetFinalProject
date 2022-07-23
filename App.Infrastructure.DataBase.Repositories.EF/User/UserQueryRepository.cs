using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
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

        public UserQueryRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<AppUser> Get(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }

        public async Task<List<AppUser>> GetAll()
        {
            var users = await _userManager.Users.Select(x => x).ToListAsync();
            return users;
        }

        public Task<AppUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user=await _userManager.FindByNameAsync(username);
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
            };
            return userDto;
        }
    }
}
