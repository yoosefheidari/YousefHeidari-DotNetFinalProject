using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public int Price { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual List<Order> Orders { get; set; }
        public virtual List<ServiceComment> ServiceComments { get; set; }
        public virtual List<ServiceFile> ServiceFiles { get; set; }

    }
}
