using App.Domain.Core.Operator.Entities;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Entities
{
    public class PhysicalFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? ExpertId { get; set; }
        public int? OrderId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }



        public virtual Expert Expert { get; set; }
        public virtual Order Order { get; set; }
    }
}
