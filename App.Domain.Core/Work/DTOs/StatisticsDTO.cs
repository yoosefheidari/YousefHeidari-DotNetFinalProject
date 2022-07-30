using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class StatisticsDTO
    {
        [Display(Name = ("کل سفارشات"))]
        public int TotalOrders { get; set; }

        [Display(Name = ("کل خدمات"))]
        public int TotalServices { get; set; }

        [Display(Name = ("کل کابران"))]
        public int TotalUsers { get; set; }
    }
}
