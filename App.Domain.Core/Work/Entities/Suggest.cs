using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Suggest
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int ExpertId { get; set; }
        public int SuggestedPrice { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Order Order { get; set; }
        public virtual AppUser Expert { get; set; }
    }
}
