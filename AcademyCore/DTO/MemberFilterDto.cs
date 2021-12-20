using System.Collections.Generic;

namespace Academy.Core.DTO
{
    public class MemberFilterDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public string Role { get; set; }
        public List<MemberCategoryDto> Category { get; set; }
        public List<LanguageFilterDto> Language { get; set; } 
        public string Last_update { get; set; }

    }
}