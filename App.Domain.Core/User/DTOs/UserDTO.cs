using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.DTOs
{
    public class UserDTO
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("نام کاربری"))]
        public string UserName { get; set; }

        [Display(Name = ("ایمیل"))]
        public string Email { get; set; }
        [Display(Name = ("نام"))]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? NationalCode { get; set; }
        public string? Mobile { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public List<string> Roles { get; set; }

    }
}
