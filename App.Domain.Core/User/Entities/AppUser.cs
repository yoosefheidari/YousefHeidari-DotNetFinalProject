using App.Domain.Core.Work.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }


        public virtual List<Order> Orders { get; set; }
        public virtual List<Suggest> Suggests { get; set; }

        public virtual List<ExpertCategory> ExpertCategories { get; set; }
        public virtual List<UserFile> UserFiles { get; set; }


    }
}
