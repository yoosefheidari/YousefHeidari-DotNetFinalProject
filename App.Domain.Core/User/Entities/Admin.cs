using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Operator.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public int IdentityUserId { get; set; }
        public string Name { get; set; } 
        public string Family { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }



    }
}
