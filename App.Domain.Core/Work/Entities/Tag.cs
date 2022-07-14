using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int TagGroupId { get; set; }
        public bool IsDeleted { get; set; }
        public bool? HasValue { get; set; }



        public virtual List<OrderTag> OrderTags { get; set; }
        public virtual TagGroup TagGroup { get; set; }

    }
}
