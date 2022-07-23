using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.User
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IUploadService _uploadService;

        public UserAppService(IUserService userService, IUploadService uploadService)
        {
            _userService = userService;
            _uploadService = uploadService;
        }

        public async Task<int> RegisterUser(UserDTO user, string password, List<IFormFile> files, CancellationToken cancellationToken, List<string>? roles)
        {
            var userId = await _userService.RegisterUser(user, password, roles);
            var fileIds = await _uploadService.UploadFileAsync(files, cancellationToken);
            var result= await _userService.AddUserFiles(userId, fileIds, cancellationToken);
            return userId;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserByUserName(username);
            return user;
        }

        public async Task<bool> LoginUser(string userName, string password, bool remember)
        {
            var result = await _userService.LoginUser(userName, password, remember);
            return result;
        }

        public async Task SignInUserById(int id)
        {
            await _userService.SignInUserById(id);
        }

        public async Task SignoutUser()
        {
            await _userService.SignoutUser();
        }

        public Task Update(AppUser user, List<string> roles, string password)
        {
            throw new NotImplementedException();
        }
    }
}
