using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class PhysicalFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }



        public virtual List<UserFile> UserFiles { get; set; }
        public virtual List<ServiceFile> ServiceFiles { get; set; }
        public virtual List<OrderFile> OrderFiles { get; set; }
    }
}
