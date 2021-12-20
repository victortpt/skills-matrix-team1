using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Repositories
{
    public class MembersRepository : BaseRepository , IMembersRepository
    {
        /// <summary>
        /// Gets an IQueryable with data from Members table.
        /// </summary>
        /// <returns>IQueryable with all data from Members table.</returns>
        public IQueryable<Member> Get() {
            return db.Members;
        }

        /// <summary>
        /// Gets an IQueryable with data from Members table joined with
        /// </summary>
        /// <returns>IQueryable with all data from Members table joined with: MemberSkills, Skills, SkillLevel, MemberLanguages, Languages, and LanguagesLevel.</returns>
        public IQueryable<Member> GetMembersSkills() {
            return db.Members.Include(b => b.MemberSkills)
                    .ThenInclude(c => c.IdSkillNavigation)
                .Include(b => b.MemberSkills)
                    .ThenInclude(f => f.IdSkillLevelNavigation)
                .Include(x=> x.MemberLanguages)
                    .ThenInclude(x => x.IdLanguageNavigation)
                .Include(x=> x.MemberLanguages)
                    .ThenInclude(y => y.IdLanguageLevelNavigation);
        }

        /// <summary>
        /// Get IQueryable with data from an specific member from Members table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IQueryable with data from an specific member from Members table</returns>
        public IQueryable<Member> Get(long id) {
            return db.Members.Where(m => m.Id == id);
        }

        /// summary>
        /// .
        /// /summary>
        /// param name="">/param>
        /// returns>/returns>
        /// response code="200">/response>
        public void Add(Member member) {

        }

        /// summary>
        /// .
        /// /summary>
        /// param name="">/param>
        /// returns>/returns>
        /// response code="200">/response>
        public void Update(Member member) {

        }

        /// summary>
        /// .
        /// /summary>
        /// param name="">/param>
        /// returns>/returns>
        /// response code="200">/response>
        public void Delete(long id) {
            //db.Members.Remove(). aqui dentro tienes que elegir el id el member en concreto
        }

    }
}
