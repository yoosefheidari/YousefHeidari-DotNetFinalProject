using App.Domain.Core.BaseData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class ServiceFile
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int FileId { get; set; }
        public bool IsDeleted { get; set; }



        public virtual Service Service { get; set; }
        public virtual PhysicalFile File { get; set; }
    }
}
