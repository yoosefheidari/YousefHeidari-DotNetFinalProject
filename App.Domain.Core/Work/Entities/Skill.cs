using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.Operator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset CreationDate { get; set; }



        public virtual List<Order> Orders { get; set; }
        public virtual List<ExpertSkill> ExpertSkills { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<SkillTagGroup> SkillTagGroups  { get; set; }
    }
}
