using System.Collections.Generic;
using Academy.Core.Repositories;
using Academy.Core.Repositories.Interfaces;
using Academy.Core.Models;
using Academy.Core.Domains.Interfaces;
using System;
using System.Linq;
using System.Text.Json;

namespace Academy.Core.Domains
{
    public class SkillLevelsDomain: ISkillLevelDomain
    {
        private readonly ISkillLevelsRepository _skillLevelsRepository;

        public SkillLevelsDomain() :
           this(new SkillLevelsRepository())
        { }

        public SkillLevelsDomain(ISkillLevelsRepository skillLevelsRepository)
        {
            _skillLevelsRepository = skillLevelsRepository;
        }

        /// <summary>
        /// Gets a list of all the skill levels in the DB
        // Retrieved data: id and skill_level (name)
        /// </summary>
        /// <returns>Returns the list of all existing skill levels</returns>
        public string GetSkillLevels()
        {
            IQueryable<SkillLevel> skillLevels = _skillLevelsRepository.Get();
            List<SkillsLevelsDto> skillsExtract = skillLevels.Select(x=> new SkillsLevelsDto{
            Id=x.Id,
            Name=x.SkillLevel1}).ToList();
            return JsonSerializer.Serialize(skillsExtract);
        }

        /// <summary>
        /// Gets a list with all the fields from all the skill levels in the DB
        /// </summary>
        /// <returns>Returns the list of all existing skill levels</returns>
        public List<SkillLevel> Get()
        {
    
            return _skillLevelsRepository.Get().ToList();
        }
        /// <summary>
        /// Returns boolean that evaluate if skill level id exists in bd.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if skill level id exists in bd, else false</returns>  
        public bool ValidateId(int id){
            return _skillLevelsRepository.Get().Where(x=>x.Id == id).Any();
        }

        /// <summary>
        /// Returns a skill_level with a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The skill level that corresponds with the indicated id</returns>
        public SkillLevel Get(int id)
        {
            return _skillLevelsRepository.Get(id).Single();
        }

        /// <summary>
        /// Updates a skill_level
        /// </summary>
        /// <param name="skillLevel"></param>
        public void Update(SkillLevel skillLevel)
        {
            if (skillLevel.SkillLevel1 == null)
            {
                throw new ArgumentNullException("Null skill_level");
            }
            _skillLevelsRepository.Update(skillLevel);
        }
        /// <summary>
        /// Adds a skill_level into the corresponding table
        /// </summary>
        /// <param name="skillLevel"></param>
        public void Add(SkillLevel skillLevel)
        {
            if (skillLevel.SkillLevel1 == null)
            {
                throw new ArgumentNullException("Null skill_level");
            }
            _skillLevelsRepository.Add(skillLevel);
        }

        /// <summary>
        /// Deletes a skill_level that corresponds with a specific id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _skillLevelsRepository.Delete(id);
        }
    }
}
