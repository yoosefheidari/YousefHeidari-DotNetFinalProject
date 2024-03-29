﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class OrderDTO
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("شناسه سرویس"))]
        public int ServiceId { get; set; }
        [Display(Name = ("شناسه دسته بندی"))]
        public int CategoryId { get; set; }

        [Display(Name = ("نام سرویس"))]
        public string ServiceName { get; set; }

        [Display(Name = ("شناسه مشتری"))]
        public int CustomerId { get; set; }

        [Display(Name = ("نام مشتری"))]
        public string CustomerName { get; set; }

        [Display(Name = ("شناسه متخصص"))]
        public int? ConfirmedExpertId { get; set; }

        [Display(Name = ("نام متخصص"))]
        public string ExpertName { get; set; }

        [Display(Name = ("وضعیت سفارش"))]
        public int? StatusId { get; set; }

        [Display(Name = ("وضعیت سفارش"))]
        public string StatusName { get; set; }

        [Display(Name = ("درصد انجام کار"))]
        public int? StatusValue { get; set; }

        [Display(Name = ("توضیحات سفارش"))]
        public string? Description { get; set; }

        [Display(Name = ("قیمت نهایی سفارش"))]
        public int? FinalPrice { get; set; }
        //public int ServiceBasePrice { get; set; }
        [Display(Name = ("تایید توسط کاربر"))]
        public bool? IsConfirmedByCustomer { get; set; }
        [Display(Name = ("تاریخ ثبت"))]
        public DateTimeOffset CreationDate { get; set; }
        [Display(Name = ("آدرس مشتری"))]
        public string? Address { get; set; }
        [Display(Name = ("حذف شده؟"))]
        public bool IsDeleted { get; set; }
        [Display(Name = ("تاریخ ثبت"))]
        public string? ShamsiCreationDate { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<SuggestDTO>? Suggests { get; set; }
        public List<PhysicalFileDTO>? Photos { get; set; }

    }
}
