using Academy.Core.Models;
using System.Linq;

namespace Academy.Core.Repositories.Interfaces
{
    public interface ILanguagesRepository
    {
        public IQueryable<Language> GetLanguages();

        public IQueryable<Language> Get(int id);

        public void Add(Language language);

        public void Update(Language language);

        public void Delete(int id);
    }
}
