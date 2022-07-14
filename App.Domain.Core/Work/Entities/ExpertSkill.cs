using App.Domain.Core.Operator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class ExpertSkill
    {
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public int SkillId { get; set; }



        public virtual Expert Expert { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
