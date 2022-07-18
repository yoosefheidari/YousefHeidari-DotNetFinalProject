using App.Domain.Core.Operator.Entities;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class ExpertCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }



        public virtual AppUser User { get; set; }
        public virtual Category Category { get; set; }
    }
}
