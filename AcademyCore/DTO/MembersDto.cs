using System.Collections.Generic;

namespace Academy.Core.DTO
{
    public class MembersDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public List<SkillsListDto> Member_skillsDto { get; set; }
        public List<LanguagesListDto> Member_languagesDto { get; set; }
        public string Last_update { get; set; }
    }
}