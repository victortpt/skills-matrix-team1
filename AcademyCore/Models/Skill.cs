

using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class Skill
    {
        public Skill()
        {
            MemberSkills = new HashSet<MemberSkill>();
        }

        public long Id { get; set; }
        public string Skill1 { get; set; }
        public long? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
