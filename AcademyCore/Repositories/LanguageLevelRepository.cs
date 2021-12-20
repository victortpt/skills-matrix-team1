using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;

namespace Academy.Core.Repositories
{
    public class LanguageLevelRepository : BaseRepository, ILanguageLevelRepository
    {
        public IQueryable<LanguageLevel> GetLanguagesProficiency()
        {
            return db.LanguageLevels;
        }

        public IQueryable<LanguageLevel> Get(int id)
        {
            return db.LanguageLevels.Where( x => x.Id == id);
        }

        public void Add(LanguageLevel language)
        {
            db.LanguageLevels.Add(language);
            db.SaveChanges();
        }

        public void Update(LanguageLevel language)
        {
            db.LanguageLevels.Update(language);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.LanguageLevels.Remove(db.LanguageLevels.Single(c => c.Id == id));
            db.SaveChanges();
        }
    }
}