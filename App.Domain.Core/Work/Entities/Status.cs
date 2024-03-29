﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StatusValue { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }



        public virtual List<Order> Orders { get; set; }
    }
}
