using System.Collections.Generic;
using Academy.Core.Repositories;
using Academy.Core.DTO;
using Academy.Core.Repositories.Interfaces;
using Academy.Core.Models;
using Academy.Core.Domains.Interfaces;
using System;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text;

namespace Academy.Core.Domains
{
    public class CategoryDomain : ICategoryDomain
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISkillRepository _skillsRepository;

        public CategoryDomain() :
           this(new CategoryRepository(),new SkillRepository())
        { }

        public CategoryDomain(ICategoryRepository categoryRepository, ISkillRepository skillRepository)
        {
            _categoryRepository = categoryRepository;
            _skillsRepository = skillRepository;
        }


        /// <summary>
        /// Get get all data from the Categories table.
        /// </summary>
        /// <returns>A list of Category objects with the data from the Categories table</returns>
        public List<Category> Get()
        {
            return _categoryRepository.GetCategory().ToList();
        }
        /// <summary>
        /// Gets data from Categories and the associated Skills.
        /// Retrieved data: id (category), category, (name) id (skill) and skill (name)
        /// </summary>
        /// <returns>An string with the data from Categories and Skills tables</returns>
        public string GetCategorySkills()
        {
            IQueryable<Category> categories =  _categoryRepository.GetCategory()
            .OrderBy(orderQuery => orderQuery.Category1);

            List<CategorySkillsDto> categorySkills= categories.Select(cat=> new CategorySkillsDto{
                    Category=cat.Category1,
                    CategoryId=cat.Id,
                    SkillsList=cat.Skills.OrderBy(orderQuery=>orderQuery.Skill1).Select(sk=>new SkillDto{
                        Skill=sk.Skill1,
                        SkillId=sk.Id
                    }).ToList()}).ToList();
            return JsonSerializer.Serialize(categorySkills);
        }

        /// <summary>
        /// Get get all data from an specific Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An object Category with the data from a Category within the Categories table</returns>
        public Category Get(int id)
        {
            return _categoryRepository.Get(id).FirstOrDefault();
        }

        /// <summary>
        /// Updates a Category from the Categories table.
        /// </summary>
        /// <param name="category"></param>
        public void Update(Category category)
        {
            if (category.Category1 == null)
            {
                throw new ArgumentNullException("Null category");
            }
            _categoryRepository.Update(category);

        }

        /// <summary>
        /// Adds a Category into the Categories table
        /// </summary>
        /// <param name="CategoryAdd"></param>
        public void Add(Category CategoryAdd)
        {
        
             _categoryRepository.Add(CategoryAdd);
            
        }

        /// <summary>
        /// Deletes a specific Category from Categories.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }
         public bool CheckCagetoryExists(Category CategoryAdd)
        {
             bool exists= _categoryRepository.GetCategory().ToList()
            .Any(cat=>RemoveAccentsWithNormalization(cat.Category1.ToLower())==RemoveAccentsWithNormalization(CategoryAdd.Category1.ToLower()));

            return exists;
        }

        public string RemoveAccentsWithNormalization(string inputString)
        {
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                     sb.Append(normalizedString[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

         public bool CheckIfAssociatedSkills(int categoryId)
        {
             bool exists= _skillsRepository.Get()
            .Any(cat=>cat.IdCategory==categoryId);

            return exists;
        }

    }
}