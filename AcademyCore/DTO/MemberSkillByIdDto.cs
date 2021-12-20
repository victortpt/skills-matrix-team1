
using Academy.Core.Models;
using System.Collections.Generic;

namespace Academy.Core.DTO
{
  public class MemberSkillbyIdDto
  {
    public string Name {get;set;}
    public long ID {get ;set;}
    public List<SkillsLevelsDto> Level {get;set;}
    public List<CategoryDto> Category {get;set;}
  }
}
