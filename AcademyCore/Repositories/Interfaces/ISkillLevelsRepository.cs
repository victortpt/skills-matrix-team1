using System.Linq;
using Academy.Core.Models;


namespace Academy.Core.Repositories.Interfaces
{
    public interface ISkillLevelsRepository
    {
        public IQueryable<SkillLevel> Get();
        public IQueryable<SkillLevel> Get(int id);
        public void Add(SkillLevel Tag);
        public void Update(SkillLevel Tag);
        public void Delete(int id);
    }
}
