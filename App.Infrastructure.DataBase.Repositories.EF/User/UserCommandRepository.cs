using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public UserCommandRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                await _signInManager.SignInAsync(user, remember);           
                return user.Id;
            }
            else
            {
                return 0;
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
            _logger.LogTrace("start of update {methodName}", "Update");
            var user1 = await _userManager.FindByIdAsync(user.Id.ToString());
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var result = await _userManager.CheckPasswordAsync(user1, oldPassword);
                if (result)
                {
                    await _userManager.ChangePasswordAsync(user1, oldPassword, newPassword);
                    _logger.LogInformation("{property} Changed Successfully","password");
                }
                else
                {
                    _logger.LogWarning("something goes wrong during changing {property} and operation faild","password");
                }

            }
            _logger.LogTrace("updating {section} of user started", "properties");
            user1.Email = user.Email;
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            user1.Mobile = user.Mobile;
            user1.PhoneNumber = user.PhoneNumber;
            user1.Address = user.Address;
            user1.NationalCode = user.NationalCode;
            user1.ProfilePicture = user.ProfilePicture;
            _logger.LogTrace("updating {section} of user ended", "properties");

            _logger.LogInformation("user {section} Changed successfully", "Properties");

            _logger.LogTrace("updating {section} of user started", "roles");
            var roles = await _userManager.GetRolesAsync(user1);
            await _userManager.RemoveFromRolesAsync(user1, roles);
            await _userManager.AddToRolesAsync(user1, user.Roles);
            _logger.LogTrace("updating {section} of user ended", "roles");
            _logger.LogInformation("user {section} Changed successfully","Roles");
            //foreach (var role in roles)
            //{
            //    if (user.Roles.Any(x => x != role))
            //    {
            //        await _userManager.RemoveFromRoleAsync(user1, role);
            //    }
            //}
            //foreach (var role in user.Roles)
            //{
            //    await _userManager.AddToRoleAsync(user1, role);
            //}
            await _userManager.UpdateAsync(user1);
            _logger.LogInformation("user {action} progress done successfully","Update");

            _logger.LogTrace("end of Update {methodName}", "Update");
        }
    }
}
