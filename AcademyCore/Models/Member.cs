using System;
using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class Member
    {
        public Member()
        {
            MemberLanguages = new HashSet<MemberLanguage>();
            MemberSkills = new HashSet<MemberSkill>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string AdId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<MemberLanguage> MemberLanguages { get; set; }
        public virtual ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
