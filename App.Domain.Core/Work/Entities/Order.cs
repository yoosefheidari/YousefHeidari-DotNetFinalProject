using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public  int CustomerId { get; set; }
        public int? ConfirmedExpertId { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public int? FinalPrice { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        //public int ServiceBasePrice { get; set; }


        public virtual Service Service  { get; set; }
        public virtual AppUser Customer { get; set; }
        public virtual AppUser Expert { get; set; }
        public virtual Status Status { get; set; }
        public virtual List<ServiceComment> Comments { get; set; }
        public virtual List<Suggest> ExpertSuggests { get; set; }
        public virtual List<OrderFile> OrderFiles { get; set; }
    }
}
