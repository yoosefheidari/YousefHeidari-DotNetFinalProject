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

        [Display(Name = ("نام خانوادگی"))]
        public string? LastName { get; set; }

        [Display(Name = ("آدرس"))]
        public string? Address { get; set; }

        [Display(Name = ("کد ملی"))]
        public string? NationalCode { get; set; }

        [Display(Name = ("موبایل"))]
        public string? Mobile { get; set; }

        [Display(Name = ("شماره تلفن"))]
        public string? PhoneNumber { get; set; }

        [Display(Name = ("عکس پروفایل"))]
        public string? ProfilePicture { get; set; }

        [Display(Name = ("رول ها"))]
        public List<string> Roles { get; set; }

    }
}
