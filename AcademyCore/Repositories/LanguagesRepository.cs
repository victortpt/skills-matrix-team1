using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Linq;

namespace Academy.Core.Repositories
{
    public class LanguagesRepository : BaseRepository, ILanguagesRepository
    {
        public IQueryable<Language> GetLanguages()
        {
            return db.Languages;
        }

        public IQueryable<Language> Get(int id)
        {
            return db.Languages.Where( x => x.Id == id);
        }

        public void Add(Language language)
        {

        }

        public void Update(Language language)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
