using Academy.Core.Models;


namespace Academy.Core.Domains.Interfaces
{
    public interface ISkillsDomain
    {
        public string GetSkills();
        public Skill Get(int id);
        public bool ValidateId(int id);
        public void Update(Skill skill);
        public void Add(Skill skill);
        public void Delete(int id);
    }
}