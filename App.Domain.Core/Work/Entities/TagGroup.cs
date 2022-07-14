using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Work.Entities
{
    public class TagGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Tag> Tags { get; set; }
        public virtual List<SkillTagGroup> SkillTagGroups { get; set; }
    }
}
