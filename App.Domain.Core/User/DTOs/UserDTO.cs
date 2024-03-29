﻿using App.Domain.Core.Work.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.Domain.Core.User.DTOs
{
    public class UserDTO
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = ("نام کاربری"))]
        [Required(ErrorMessage =("وارد کردن نام کاربری الزامیست"))]
        public string UserName { get; set; }

        [Display(Name = ("رمز عبور"))]
        [Required(ErrorMessage ="رمز عبور حداقل 3 حرف باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = ("تکرار رمز عبور"))]
        [Required(ErrorMessage = "تکرار رمز عبور حداقل 3 حرف باشد")]
        [DataType(DataType.Password)]
        
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="رمز عبور و تکرار رمز عبور متفاوت هستند")]
        public string ConfirmPassword { get; set; }

        [Display(Name = ("ایمیل"))]
        [Required(ErrorMessage = ("وارد کردن ایمیل الزامیست"))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        

        [Display(Name = ("نام"))]
        [Required(ErrorMessage = ("وارد کردن نام الزامیست"))]
        public string? FirstName { get; set; }

        [Display(Name = ("نام خانوادگی"))]
        [Required(ErrorMessage = ("وارد کردن نام خانوادگی الزامیست"))]
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
        public List<string>? Roles { get; set; }

        public List<CategoryDTO>? expertCategories { get; set; }
        public List<OrderDTO>? UserOrders { get; set; }
        public List<CommentDTO>? UserComments { get; set; }

    }
}
