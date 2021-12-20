using Academy.Core.Models;
using Academy.Core.DTO;
using Academy.Core.Domains.Interfaces;
using Academy.Core.Repositories.Interfaces;
using Academy.Core.Repositories;
using System.Linq;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Academy.Core.Domains
{
    public class LanguageLevelDomain : ILanguageLevelDomain
    {
        private readonly ILanguageLevelRepository _languageLevelRepository;

        public LanguageLevelDomain() :
           this(new LanguageLevelRepository())
        { }

        public LanguageLevelDomain(ILanguageLevelRepository languageLevelRepository)
        {
            _languageLevelRepository = languageLevelRepository;
        }

        public List<LanguageLevelDto> GetLanguagesProficiency()
        {
            return _languageLevelRepository.GetLanguagesProficiency().Select( x=> new LanguageLevelDto {
                ID = x.Id,
                Name = x.Level,
            }).ToList();

        }        
        /// <summary>
        /// Get the language proficiency data by id
        /// </summary>  
        /// <returns>Language proficiency object</returns>
        /// <param name="id"></param>        
        public LanguageLevel Get(int id)
        {
            return _languageLevelRepository.Get(id).FirstOrDefault();
        }

        /// <summary>
        /// Updates a languageLevel
        /// </summary>
        /// <param name="languageLevel"></param>        
        public void Update(LanguageLevel languageLevel)
        {
            if (languageLevel.Level == null)
            {
                throw new ArgumentNullException("Null skill_level");
            }
            _languageLevelRepository.Update(languageLevel);
        }

        /// <summary>
        /// Adds a language level into the corresponding table
        /// </summary>
        /// <param name="languageLevel"></param>
        public void Add(LanguageLevel languageLevel)
        {
            if (languageLevel.Level == null)
            {
                throw new ArgumentNullException("Null skill_level");
            }
            _languageLevelRepository.Add(languageLevel);
        }

        /// <summary>
        /// Deletes a Language level that corresponds with a specific id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _languageLevelRepository.Delete(id);
        }

    }
}