using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }


        public virtual List<Service> Services { get; set; }
        public virtual List<ExpertCategory> ExpertCategories { get; set; }
    }
}
