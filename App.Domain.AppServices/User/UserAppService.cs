﻿using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.Contracts.Services;
using Microsoft.AspNetCore.Http;
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
        private readonly IUploadService _uploadService;
        private readonly ILogger _logger;

        public UserAppService(IUserService userService, IUploadService uploadService, ILogger logger)
        {
            _userService = userService;
            _uploadService = uploadService;
            _logger = logger;
        }

        public async Task<int> RegisterUser(UserDTO user, string password, List<IFormFile> files, CancellationToken cancellationToken)
        {
            var userId = await _userService.RegisterUser(user, password);
            var fileIds = await _uploadService.UploadFileAsync(files, cancellationToken);
            var result= await _userService.AddUserFiles(userId, fileIds, cancellationToken);
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
            var users = await _userService.GetAll(id,search, cancellationToken);
            return users;
        }

        public Task<UserDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
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

        public async Task Update(UserDTO user, string oldPassword, string newPassword)
        {
            _logger.LogTrace("Call update {appServiceName} for user", "update");
            await _userService.Update(user, oldPassword, newPassword);
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _userService.GetRoles();
            return roles;
        }
    }
}
