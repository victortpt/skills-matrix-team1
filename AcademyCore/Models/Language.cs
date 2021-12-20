

using System.Collections.Generic;

namespace Academy.Core.Models
{
    public partial class Language
    {
        public Language()
        {
            MemberLanguages = new HashSet<MemberLanguage>();
        }

        public long Id { get; set; }
        public string Language1 { get; set; }
        public string LanguageCode { get; set; }

        public virtual ICollection<MemberLanguage> MemberLanguages { get; set; }
    }
}
