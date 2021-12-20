using Academy.Core.Models;
using System.Linq;

namespace Academy.Core.Repositories.Interfaces
{
    public interface ILanguageLevelRepository
    {
        public IQueryable<LanguageLevel> GetLanguagesProficiency();

        public IQueryable<LanguageLevel> Get(int id);

        public void Add(LanguageLevel languageLevel);

        public void Update(LanguageLevel languageLevel);

        public void Delete(int id);
    }
}