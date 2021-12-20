using System.Collections.Generic;
using Academy.Core.Models;

namespace Academy.Core.DTO
{
    public class MemberSkillDto
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Level { get; set; }
        public long LevelId { get; set; }
    }
}