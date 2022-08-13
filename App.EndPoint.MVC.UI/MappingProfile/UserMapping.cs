using App.Domain.Core.User.DTOs;
using App.EndPoint.MVC.UI.Models;

namespace App.EndPoint.MVC.UI.MappingProfile
{
    public static class UserMapping
    {
        public static UserDTO MapUserViewModelToUserDto(RegisterUserViewModel userViewModel)
        {
            var userDto = new UserDTO()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Password = userViewModel.Password,
                ConfirmPassword = userViewModel.ConfirmPassword,
            };
            return userDto;
        }
    }
}
