using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Operator.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int IdentityUserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }

        //public string Email { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }




        public virtual List<Order> Orders { get; set; }
        
    }
}
