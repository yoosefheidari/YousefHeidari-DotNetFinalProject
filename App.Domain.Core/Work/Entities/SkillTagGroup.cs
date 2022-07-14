using App.Domain.Core.BaseData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class SkillTagGroup
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int TagGroupId { get; set; }


        public virtual Skill Skill { get; set; }  
        public virtual TagGroup TagGroup { get; set; }
    }
}
