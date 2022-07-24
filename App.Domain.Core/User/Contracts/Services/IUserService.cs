﻿using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll(int id, string? search, CancellationToken cancellationToken);
        Task<UserDTO> Get(int id);
        Task<UserDTO> GetUserByUserName(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<int> RegisterUser(UserDTO user, string password);
        Task Update(UserDTO user, string password);
        Task Delete(int id);
        Task SignInUserById(int id);
        Task SignoutUser();
        Task<bool> LoginUser(string userName, string password, bool remember);
        Task<bool> AddUserFiles(int userId, List<int> files,CancellationToken cancellationToken);
    }
}
