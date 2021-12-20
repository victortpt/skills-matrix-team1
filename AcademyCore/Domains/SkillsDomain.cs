using System.Collections.Generic;
using Academy.Core.Repositories;
using Academy.Core.DTO;
using Academy.Core.Repositories.Interfaces;
using Academy.Core.Models;
using Academy.Core.Domains.Interfaces;
using System;
using System.Linq;
using System.Text.Json;
namespace Academy.Core.Domains
{
    public class SkillsDomain : ISkillsDomain
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsDomain() :
           this(new SkillRepository())
        { }

        public SkillsDomain(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        /// <summary>
        /// Gets a list of all the skills in the DB
        // Retrieved data: id, skill, id_category
        /// </summary>
        /// <returns>Returns the list of all existing skills</returns>      
        public string GetSkills(){
            return JsonSerializer.Serialize(_skillRepository.Get().ToList());
        }

        /// <summary>
        /// Returns a skill with a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The skill that corresponds with the indicated id</returns>        
        public Skill Get(int id){
            return _skillRepository.Get().Where(x=>x.Id == id).Single();
        }

        /// <summary>
        /// Returns boolean that evaluate if skill id exists in bd.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if skill id exists in bd, else false</returns>   
        public bool ValidateId(int id){
            return _skillRepository.Get().Where(x=>x.Id == id).Any();
        }

        /// <summary>
        /// Updates a skill
        /// </summary>
        /// <param name="skill"></param>
        public void Update(Skill skill){
            if (skill.Skill1 == null)
            {
                throw new ArgumentNullException("Null skill");
            }
            _skillRepository.Update(skill);            
        }

        /// <summary>
        /// Adds a skill into the corresponding table
        /// </summary>
        /// <param name="skill"></param>        
        public void Add(Skill skill){
            if (skill.Skill1 == null)
            {
                throw new ArgumentNullException("Null skill");
            }
            _skillRepository.Add(skill);            
        }

        /// <summary>
        /// Deletes a skill that corresponds with a specific id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id){
            _skillRepository.Delete(id);
        }

    }
}