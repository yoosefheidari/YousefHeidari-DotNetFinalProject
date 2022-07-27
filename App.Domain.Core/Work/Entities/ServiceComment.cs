using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class ServiceComment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsApproved { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        //public int? ServiceId { get; set; }




        public virtual Order Order { get; set; }
        //public virtual Service Service { get; set; }

    }
}
