
namespace Academy.Core.Models
{
    public partial class MemberLanguage
    {
        public long Id { get; set; }
        public long? IdMember { get; set; }
        public long? IdLanguage { get; set; }
        public long? IdLanguageLevel { get; set; }

        public virtual LanguageLevel IdLanguageLevelNavigation { get; set; }
        public virtual Language IdLanguageNavigation { get; set; }
        public virtual Member IdMemberNavigation { get; set; }
    }
}
