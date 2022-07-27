using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? ConfirmedExpertId { get; set; }
        public string ExpertName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? StatusValue { get; set; }
        public string? Description { get; set; }
        public int? FinalPrice { get; set; }
        //public int ServiceBasePrice { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
