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

namespace Academy.Core.Domains
{
    public class MemberSkillsDomain : BaseRepository, IMemberSkillsDomain 
    {
        private readonly IMembersRepository _membersRepository;
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IMemberSkillRepository _memberSkillRepository;
        private readonly ISkillRepository _skillsRepository;
        private readonly ISkillLevelsRepository _skillLevelsRepository;
        private readonly ILanguagesRepository _languagesRespository;

        public MemberSkillsDomain() :
           this(new MembersRepository(), new CategoryRepository(), new MemberSkillRepository(), new SkillRepository(), new SkillLevelsRepository(),new LanguagesRepository())
        { }

        public MemberSkillsDomain(IMembersRepository memberRepository, ICategoryRepository categoriesRepository, IMemberSkillRepository memberSkillRepository, ISkillRepository skillsRepository, ISkillLevelsRepository skillLevelsRepository, ILanguagesRepository languagesRepository)
        {
            _membersRepository = memberRepository;
            _categoriesRepository = categoriesRepository;
            _memberSkillRepository = memberSkillRepository;
            _skillsRepository = skillsRepository;
            _skillLevelsRepository = skillLevelsRepository;
            _languagesRespository = languagesRepository;
        }

        /// <summary>
        /// Gets a list of all the members DB.
        /// </summary>
        /// <returns>Returns the list of all members and their relevant data</returns>
        public List<MemberSkill> Get()
        {
            return _memberSkillRepository.Get().ToList();
        }

        public void Update(MemberSkill members)
        {

        }
        public void Add(MemberSkill members)
        {
            
        }
        public void Delete(long id)
        {
            
        }
    }
}