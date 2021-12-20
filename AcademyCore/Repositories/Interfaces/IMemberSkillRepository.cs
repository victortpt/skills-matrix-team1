using System.Linq;
using Academy.Core.Models;


namespace Academy.Core.Repositories.Interfaces
{
    public interface IMemberSkillRepository
    {
        public IQueryable<MemberSkill> Get();
        public IQueryable<MemberSkill> Get(long idMember, long idSkill, long idLevel);
        public void Add(MemberSkill memberSkill);
        public void Update(MemberSkill memberSkill);
        public void Delete(long idMember, long idSkill);
    }
}