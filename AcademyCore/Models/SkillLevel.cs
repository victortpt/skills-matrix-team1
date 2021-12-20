
using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class SkillLevel
    {
        public SkillLevel()
        {
            MemberSkills = new HashSet<MemberSkill>();
        }

        public long Id { get; set; }
        public string SkillLevel1 { get; set; }

        public int NumericalSkillLevel { get; set; }
        public virtual ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
