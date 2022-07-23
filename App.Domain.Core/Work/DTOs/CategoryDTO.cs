using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class CategoryDTO
    {
        [Display(Name =("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("نام دسته بندی"))]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        [DataType(DataType.Date)]
        public DateTimeOffset CreationDate { get; set; }
        public List<ServiceDTO> Services { get; set; }

        public bool IsDeleted { get; set; }
    }
}
