using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.Operator.Entities;
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
        public  int CustomerId { get; set; }
        public int SkillId { get; set; }
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


        public virtual Skill Skill { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Expert Expert { get; set; }
        public virtual Status Status { get; set; }
        public virtual Opinion Opinion { get; set; }
        public virtual List<OrderTag> OrderTags { get; set; }
        public virtual List<ExpertSuggest> ExpertSuggests { get; set; }
        public virtual List<PhysicalFile> PhysicalFiles { get; set; }
    }
}
