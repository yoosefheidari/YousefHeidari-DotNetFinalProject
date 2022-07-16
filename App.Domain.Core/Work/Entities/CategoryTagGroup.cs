using App.Domain.Core.BaseData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class CategoryTagGroup
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TagGroupId { get; set; }


        public virtual Category Category { get; set; }  
        public virtual TagGroup TagGroup { get; set; }
    }
}
