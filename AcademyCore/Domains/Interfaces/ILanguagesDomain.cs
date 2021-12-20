using System.Collections.Generic;
using Academy.Core.DTO;
using Academy.Core.Models;

namespace Academy.Core.Domains.Interfaces
{
    public interface ILanguagesDomain
    {
        public string GetLanguages();
        public Language Get(int id);
        public void Update(Language language);
        public void Add(Language language);
        public void Delete(int id);

    }
}