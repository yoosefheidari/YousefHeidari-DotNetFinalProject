using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public int IdentityUserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }

        //public string Email { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
