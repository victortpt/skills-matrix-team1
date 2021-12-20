using System;
using System.Collections.Generic;

namespace Academy.Core.DTO
{
    public class CategorySkillsDto
    {
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public List<SkillDto> SkillsList { get; set; }
    }
}