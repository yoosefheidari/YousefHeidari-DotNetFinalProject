using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.User
{
    public class UserSerivce : IUserService
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserFileCommandRepository _userFileCommandRepository;
        private readonly ILogger<UserSerivce> _logger;
        private readonly IHttpContextAccessor _httpContext;

        public UserSerivce(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IUserFileCommandRepository userFileCommandRepository, ILogger<UserSerivce> logger, IHttpContextAccessor httpContext)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _userFileCommandRepository = userFileCommandRepository;
            _logger = logger;
            _httpContext = httpContext;
        }

        public async Task<int> RegisterUser(UserDTO user, string password)
        {

            var result = await _userCommandRepository.Add(user, password);
            return result;
        }

        public async Task Delete(int id)
        {
            await _userCommandRepository.Delete(id);
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await _userQueryRepository.Get(id);
            return user;
        }

        public async Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken)
        {
            var users = await _userQueryRepository.GetAll(id, search, cancellationToken);
            return users;
        }


        public async Task<UserDTO> GetUserByUserName(string username)
        {
            var user = await _userQueryRepository.GetUserByUserName(username);
            return user;
        }

        public async Task<int> LoginUser(string userName, string password, bool remember)
        {
            var result = await _userCommandRepository.LoginUser(userName, password, remember);
            return result;            
        }

        public async Task SignInUserById(int id)
        {
            var isPersistent = true;
            await _userCommandRepository.SignInUserById(id, isPersistent);
        }

        public async Task SignoutUser()
        {
            await _userCommandRepository.SignoutUser();
        }

        public async Task Update(UserDTO user, string oldPassword, string newPassword)
        {
            _logger.LogTrace("Call update {serviceName} for user", "update");
            await _userCommandRepository.Update(user, oldPassword, newPassword);
        }

        public async Task<bool> AddUserFiles(int userId, List<int> files, CancellationToken cancellationToken)
        {
            foreach (var fileId in files)
            {
                UserFileDTO userFile = new()
                {
                    FileId = fileId,
                    UserId = userId,
                    IsDeleted = false,
                };
                var id = await _userFileCommandRepository.Add(userFile, cancellationToken);
            }
            return true;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _userQueryRepository.GetRoles();
            return roles;
        }

        public async Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken)
        {
            await _userCommandRepository.UpdateExpertSkills(userId, categories, cancellationToken);
        }

        public async Task<UserDTO> GetCurrentUserFullInfo()
        {
            var username = _httpContext.HttpContext.User.Identity.Name;
            var userDto=await _userQueryRepository.GetUserByUserName(username);
            return userDto;
        }

        public async Task UpdateProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            await _userCommandRepository.UpdateProfilePicture(user, cancellationToken);
        }
    }
}
