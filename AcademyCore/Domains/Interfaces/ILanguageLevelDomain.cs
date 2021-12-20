using System.Collections.Generic;
using Academy.Core.DTO;
using Academy.Core.Models;

namespace Academy.Core.Domains.Interfaces
{
    public interface ILanguageLevelDomain
    {
        //public string GetLanguagesProficiency();
        public List<LanguageLevelDto> GetLanguagesProficiency();
        public LanguageLevel Get(int id);
        public void Update(LanguageLevel languageLevel);
        public void Add(LanguageLevel languageLevel);
        public void Delete(int id);

    }
}