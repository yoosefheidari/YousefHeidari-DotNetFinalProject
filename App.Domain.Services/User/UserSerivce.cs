using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
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

        public UserSerivce(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IUserFileCommandRepository userFileCommandRepository)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _userFileCommandRepository = userFileCommandRepository;
        }

        public async Task<int> RegisterUser(UserDTO user, string password, List<string>? roles)
        {
            var roleResult = roles.Any(x => x == "Customer");
            if (!roleResult)
                roles.Add("Customer");

            var result=await _userCommandRepository.Add(user, password, roles);
            return result;
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
            var user=await _userQueryRepository.GetUserByUserName(username);
            return user;
        }

        public async Task<bool> LoginUser(string userName, string password, bool remember)
        {
            var result=await _userCommandRepository.LoginUser(userName, password, remember);
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

        public Task Update(AppUser user, List<string> roles, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddUserFiles(int userId, List<int> files, CancellationToken cancellationToken)
        {
            foreach(var fileId in files)
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
    }
}
