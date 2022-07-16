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
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }
        public string? Description { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? ConfirmedExpertId { get; set; }
        //public bool? IsConfirmedByExpert { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public string? SuggestedWorkTimeByExpert { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? FinalizedDate { get; set; }
        public DateTimeOffset RequestedDate { get; set; }
    }
}
