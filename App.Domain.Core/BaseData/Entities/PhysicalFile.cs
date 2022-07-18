﻿using App.Domain.Core.User.Entities;
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



        public virtual List<UserFile> UserFiles { get; set; }
        public virtual List<ServiceFile> ServiceFiles { get; set; }
        public virtual List<OrderFile> OrderFiles { get; set; }
    }
}
