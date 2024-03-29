﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class ServiceDTO
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }
        [Display(Name = ("دسته بندی سرویس"))]
        public int CategoryId { get; set; }
        [Display(Name = ("نام سرویس"))]
        public string Title { get; set; } = null!;
        [Display(Name = ("توضیحات"))]
        public string? ShortDescription { get; set; }
        [Display(Name = ("قیمت سرویس"))]
        public int Price { get; set; }
        [Display(Name = ("تاریخ ثبت"))]
        public DateTimeOffset CreationDate { get; set; }
        [Display(Name = ("تاریخ ثبت"))]
        public string? ShamsiCreationDate { get; set; }
    }
}
