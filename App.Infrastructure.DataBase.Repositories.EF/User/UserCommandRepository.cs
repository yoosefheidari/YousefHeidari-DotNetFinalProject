using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.User
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UserCommandRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<int> Add(UserDTO user, string password)
        {
            var newUser = new AppUser();
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Address = user.Address;
            newUser.NationalCode = user.NationalCode;
            newUser.Mobile = user.Mobile;
            newUser.PhoneNumber = user.PhoneNumber;
            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(newUser, "Customer");
            return newUser.Id;


        }



        public async Task Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task<bool> LoginUser(string userName, string password, bool remember)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                await _signInManager.SignInAsync(user, remember);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task SignInUserById(int id, bool isPersistent)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _signInManager.SignInAsync(user, isPersistent);

        }

        public async Task SignoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Update(UserDTO user, string oldPassword, string newPassword)
        {
            var user1 = await _userManager.FindByIdAsync(user.Id.ToString());
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var result = await _userManager.CheckPasswordAsync(user1, oldPassword);
                if (result)
                    await _userManager.ChangePasswordAsync(user1, oldPassword, newPassword);

            }

            user1.Email = user.Email;
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            var roles = await _userManager.GetRolesAsync(user1);
            foreach (var role in roles)
            {
                if (user.Roles.Any(x => x != role))
                {
                    await _userManager.RemoveFromRoleAsync(user1, role);
                }
            }
            foreach (var role in user.Roles)
            {
                await _userManager.AddToRoleAsync(user1, role);
            }
            await _userManager.UpdateAsync(user1);
        }
    }
}
