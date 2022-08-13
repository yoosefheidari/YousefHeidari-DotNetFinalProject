using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.Contracts.Services;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserAppService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        public UserAppService(IUserService userService, ILogger<UserAppService> logger, IConfiguration configuration, IFileService fileService)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<int> RegisterUser(UserDTO user, string password, CancellationToken cancellationToken)
        {
            var userId = await _userService.RegisterUser(user, password);
            return userId;
        }

        public async Task Delete(int id)
        {
            await _userService.Delete(id);
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await _userService.Get(id);
            return user;
        }

        public async Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAll(id, search, cancellationToken);
            return users;
        }

        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserByUserName(username);
            return user;
        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
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

        public async Task Update(UserDTO user, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Call update {appServiceName} for user", "update");
            await _userService.Update(user, oldPassword, newPassword);

        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _userService.GetRoles();
            return roles;
        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            await _userService.UpdateExpertSkills(userId, categories, cancellationToken);
        }

        public async Task<UserDTO> GetCurrentUserFullInfo()
        {
            var user = await _userService.GetCurrentUserFullInfo();

            var filrRootPath = _configuration.GetSection("DownloadPath").Value;
            foreach (var order in user.UserOrders)
            {
                foreach (var file in order.Photos)
                {
                    file.Path = filrRootPath + "/" + file.Path;
                }
            }
            user.ProfilePicture = filrRootPath + "/" + user.ProfilePicture;
            return user;
        }

        public async Task ChangeProfilePicture(IFormFile file, CancellationToken cancellationToken)
        {
            var currentUser = await _userService.GetCurrentUserFullInfo();
            List<IFormFile> files = new();
            files.Add(file);
            await _fileService.DeletePhysicalFile(currentUser.ProfilePicture, cancellationToken);
            var ids = await _fileService.UploadFileAsync(files, cancellationToken);
            var filee = await _fileService.Get(ids[0], cancellationToken);
            currentUser.ProfilePicture = filee.Path;
            await _userService.UpdateProfilePicture(currentUser, cancellationToken);
        }

        public async Task<bool> EnsureUserIsNotExist(UserDTO user, CancellationToken cancellationToken)
        {
            var userByUsername = await _userService.GetUserByUserName(user.UserName);
            var userByEmail = await _userService.GetUserByEmail(user.Email);
            if (userByUsername != null || userByEmail != null)
                return false;
            return true;

        }

        public async Task<bool> EnsureUserNameIsNotExist(string username, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserName(username);
            if (user != null)
                return false;
            return true;
        }

        public async Task<bool> EnsureEmailIsNotExist(string email, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user != null)
                return false;
            return true;
        }
    }
}
