using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class CommentDTO
    {
        [Display(Name = ("تاریخ ثبت"))]
        public int Id { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public int OrderId { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public int? ServiceId { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public string Title { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public string Description { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public bool? IsApproved { get; set; }
    }
}
