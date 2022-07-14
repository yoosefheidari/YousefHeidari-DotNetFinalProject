using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class ExpertSuggestDTO
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int ExpertId { get; set; }
        public decimal Price { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
