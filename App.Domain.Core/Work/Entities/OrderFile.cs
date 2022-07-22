using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class OrderFile
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FileId { get; set; }
        public bool IsDeleted { get; set; }



        public virtual Order Order { get; set; }
        public virtual PhysicalFile File { get; set; }
    }
}
