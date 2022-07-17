using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class OrderTag
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TagId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Order Order { get; set; }
    }
}
