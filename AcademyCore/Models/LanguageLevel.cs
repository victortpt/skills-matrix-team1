

using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class LanguageLevel
    {
        public LanguageLevel()
        {
            MemberLanguages = new HashSet<MemberLanguage>();
        }

        public long Id { get; set; }
        public string Level { get; set; }

        public virtual ICollection<MemberLanguage> MemberLanguages { get; set; }
    }
}
