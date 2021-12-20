using Academy.Core.Models;
using System.Collections.Generic;


namespace Academy.Core.Domains.Interfaces
{
    public interface ISkillLevelDomain
    {
        public string  GetSkillLevels();
        public SkillLevel Get(int id);
        public void Update(SkillLevel SkillLevel);
        public void Add(SkillLevel SkillLevel);
        public void Delete(int id);
        public bool ValidateId(int id);
    }
}

