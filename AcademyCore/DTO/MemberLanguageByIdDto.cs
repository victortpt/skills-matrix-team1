using System.Collections.Generic;


namespace Academy.Core.DTO
{
    public class MemberLanguageByIdDto
    
    {
        public string Name {get;set;}
        public string Code {get;set;}
        public long ID {get;set;}
        public List<LanguageLevelDto> Level {get;set;}



    }
}