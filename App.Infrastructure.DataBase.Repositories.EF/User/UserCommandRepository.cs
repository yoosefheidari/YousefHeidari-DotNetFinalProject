using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using App.Domain.Core.Work.Entities;
using App.Infrastructure.DataBase.SqlServer;
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
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserCommandRepository> _logger;
        private readonly AppDbContext _appDbContext;

        public UserCommandRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<UserCommandRepository> logger, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public async Task<int> Add(UserDTO user, string password)
        {
            try
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
                {
                    await _userManager.AddToRoleAsync(newUser, "Custommer");
                    _logger.LogInformation("new user {username} is {action} successfully", newUser.UserName, "create");
                }
                return newUser.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "فرایند {عملیات} کاربر با خطا روبه رو شد", "ثبت نام");
                throw new Exception("فرایند ثبت نام کاربر با خطا روبه رو شد");
            }
        }



        public async Task Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                await _userManager.DeleteAsync(user);
                _logger.LogInformation("user with id {id} is {action} successfully", id, "delete");
            }
            catch (Exception ex)
            {
                throw new Exception("امکان حذف به دلیل استفاده شناسه وجود ندارد", ex.InnerException);
            }
            
        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                _logger.LogInformation("{username} {action} successfully", userName, "login");
                await _signInManager.SignInAsync(user, remember);
                return user.Id;
            }
            else
            {
                _logger.LogWarning("{username} {action} process failed", userName, "login");
                return 0;
            }

        }

        public async Task SignInUserById(int id, bool isPersistent)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            _logger.LogInformation("کاربری با شناسه کاربری {آی دی} {عملیات} سایت شد", id, "وارد");
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
                {
                    await _userManager.ChangePasswordAsync(user1, oldPassword, newPassword);
                    _logger.LogInformation("رمز عبور کاربر {نام کاربری} با موفقیت {عملیات} شد", user.UserName, "به روز رسانی");
                }
                else
                {
                    _logger.LogWarning("خطایی در {عملیات} رمز عبور کاربر {نام کاربری} رخ داد", "به روز رسانی", user.UserName);
                }

            }
            user1.Email = user.Email;
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            user1.Mobile = user.Mobile;
            user1.PhoneNumber = user.PhoneNumber;
            user1.Address = user.Address;
            user1.NationalCode = user.NationalCode;
            user1.ProfilePicture = user.ProfilePicture;
            var roles = await _userManager.GetRolesAsync(user1);
            await _userManager.RemoveFromRolesAsync(user1, roles);
            await _userManager.AddToRolesAsync(user1, user.Roles);
            await _userManager.UpdateAsync(user1);
            _logger.LogInformation("{عملیات} مشخصات کاربر با نام کاربری {نام کاربری} با موفقیت انجام شد", "به روز رسانی", user.UserName);

        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.ExpertCategories.Where(c => c.ExpertId == userId).ToListAsync();

            foreach (var item in categories)
            {
                if (!result.Select(x => x.CategoryId).Contains(item))
                {
                    ExpertCategory skill = new()
                    {

                        IsDeleted = false,
                        CategoryId = item,
                        ExpertId = userId,
                    };
                    await _appDbContext.ExpertCategories.AddAsync(skill, cancellationToken);
                }
            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{عملیات} مشخصات کاربر با شناسه کاربری {شناسه کاربری} با موفقیت انجام شد", "به روز رسانی مهارت", userId);
        }

        public async Task UpdateProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            var yser = await _userManager.FindByIdAsync(user.Id.ToString());
            yser.ProfilePicture = user.ProfilePicture;
            await _userManager.UpdateAsync(yser);
            _logger.LogInformation("{عملیات} مشخصات کاربر با نام کاربری {نام کاربری} با موفقیت انجام شد", "به روز رسانی تصویر پروفایل", user.UserName);
        }
    }
}
