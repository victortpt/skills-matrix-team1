using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;

namespace Academy.Core.Repositories
{
    public class MemberSkillRepository : BaseRepository , IMemberSkillRepository
    {
       
        
        public IQueryable<MemberSkill> Get() {
            return db.MemberSkills;
        }
        public IQueryable<MemberSkill> Get(int id) {
            return db.MemberSkills.Where(m => m.Id == id);
        }
        public IQueryable<MemberSkill> Get(long idMember, long idSkill, long idLevel) {
            return db.MemberSkills.Where(m => m.IdMember == idMember & m.IdSkill == idSkill & m.IdSkillLevel == idLevel);
        }
        public void Add(MemberSkill newMemberSkill) {
            db.MemberSkills.Add(newMemberSkill);
            db.SaveChanges();
        }

        public void Update(MemberSkill updatedMemberSkill) {
            db.MemberSkills.Update(updatedMemberSkill);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete member skills from repository
        /// </summary>
        /// <param name="memberSkill"></param>
        /// <returns>Boolean</returns>
        public void Delete(long idMember, long idSkill)
        {
            MemberSkill memberSkillToDelete = db.MemberSkills.Where(ms=>ms.IdMember == idMember & ms.IdSkill == idSkill).FirstOrDefault();
            db.MemberSkills.Remove(memberSkillToDelete);
            db.SaveChanges();
        }
    }
}