﻿using App.Domain.Core.User.DTOs;
using App.Domain.Core.User.Entities;
using App.Domain.Core.Work.DTOs;
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
        Task<UserDTO>? GetUserByEmail(string email);
        Task<int> RegisterUser(UserDTO user, string password);
        Task Update(UserDTO user, string oldPassword, string newPassword);
        Task UpdateExpertSkills(int userId, List<int> categories, CancellationToken cancellationToken);
        Task Delete(int id);
        Task SignInUserById(int id);
        Task SignoutUser();
        Task<int> LoginUser(string userName, string password, bool remember);
        Task<bool> AddUserFiles(int userId, List<int> files,CancellationToken cancellationToken);
        Task<List<RoleDTO>> GetRoles();
        Task<UserDTO> GetCurrentUserFullInfo();
        Task UpdateProfilePicture(UserDTO user,CancellationToken cancellationToken);
        Task<bool> EnsureUserIsNotExist(UserDTO user, CancellationToken cancellationToken);
        Task<List<CommentDTO>> GetExpertRatingAndComments(int expertId, CancellationToken cancellationToken);

        Task<List<CategoryDTO>?> GetExpertSkills(string username, CancellationToken cancellationToken);
        Task<List<string>?> GetUserRoles(string username, CancellationToken cancellationToken);
        Task<List<OrderDTO>?> GetUserOrders(string username, CancellationToken cancellationToken);
        Task<List<SuggestDTO>?> GetOrderSuggests(int orderId, CancellationToken cancellationToken);
        Task<List<CommentDTO>?> GetOrderComments(int orderId, CancellationToken cancellationToken);

        Task<UserDTO> GetCurrentUserBriefInfoByUsername(string username, CancellationToken cancellationToken);
    }
}
