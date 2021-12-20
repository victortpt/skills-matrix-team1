using System.Linq;
using Academy.Core.Models;


namespace Academy.Core.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        public IQueryable<Skill> Get();
        public void Add(Skill Tag);
        public void Update(Skill Tag);
        public void Delete(int id);
    }
}