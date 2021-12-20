
namespace Academy.Core.Models
{
    public partial class MemberSkill
    {
        public long Id { get; set; }
        public long? IdMember { get; set; }
        public long? IdSkill { get; set; }
        public long? IdSkillLevel { get; set; }

        public virtual Member IdMemberNavigation { get; set; }
        public virtual SkillLevel IdSkillLevelNavigation { get; set; }
        public virtual Skill IdSkillNavigation { get; set; }
    }
}
