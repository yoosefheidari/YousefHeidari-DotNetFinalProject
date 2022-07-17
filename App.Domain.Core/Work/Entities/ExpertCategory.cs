using App.Domain.Core.Operator.Entities;
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
        public int ExpertId { get; set; }
        public bool IsDeleted { get; set; }



        public virtual Expert Expert { get; set; }
        public virtual Category Category { get; set; }
    }
}
