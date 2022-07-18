using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DataBase.Repositories.EF.User.Admin
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly UserManager<IdentityUser<int>> _userManager;

        public UserCommandRepository(UserManager<IdentityUser<int>> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> Add(AppUser user,List<string> roles,string password)
        {
            var newUser=new AppUser();
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.FirstName=user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Address = user.Address;
            newUser.NationalCode = user.NationalCode;
            newUser.Mobile = user.Mobile;
            newUser.PhoneNumber = user.PhoneNumber;
            var result=await _userManager.CreateAsync(newUser,password);
            roles.ForEach(async x=>await _userManager.AddToRoleAsync(newUser,x));        
            return newUser.Id;
            

        }

        public async Task Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task Update(AppUser user, List<string> roles, string password)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
