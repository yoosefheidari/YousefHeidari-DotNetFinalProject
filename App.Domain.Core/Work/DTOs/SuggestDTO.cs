using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class SuggestDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public string? ExpertName { get; set; }
        public int SuggestedPrice { get; set; }
        public string? Description { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
