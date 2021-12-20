using Academy.Core.Models;
using Academy.Core.DTO;
using Academy.Core.Domains.Interfaces;
using Academy.Core.Repositories.Interfaces;
using Academy.Core.Repositories;
using System.Linq;
using System.Text.Json;

namespace Academy.Core.Domains
{
    public class LanguagesDomain : ILanguagesDomain
    {
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesDomain() :
           this(new LanguagesRepository())
        { }

        public LanguagesDomain(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        /// <summary>
        /// Get the languages list ordered by language (name) alphabetically
        /// </summary>
        /// <returns>Json object</returns>
        public string GetLanguages()
        {
            return JsonSerializer.Serialize(_languagesRepository.GetLanguages().OrderBy( x=> x.Language1).Select( x=> new LanguagesDto {
                Id = x.Id,
                Language = x.Language1,
                LanguageCode = x.LanguageCode
            }).ToList());

        }
        /// <summary>
        /// Get the language data by id
        /// </summary>  
        /// <returns>Language object</returns>
        /// <param name="id"></param>        
        public Language Get(int id)
        {
            return _languagesRepository.Get(id).FirstOrDefault();
        }
        public void Update(Language language)
        {

        }
        public void Add(Language language)
        {

        }
        public void Delete(int id)
        {

        }

    }
}