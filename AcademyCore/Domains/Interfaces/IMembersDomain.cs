using System;
using System.Collections.Generic;
using Academy.Core.Models;
using Academy.Core.DTO;


namespace Academy.Core.Domains.Interfaces
{
    public interface IMembersDomain
    {
        public string GetMembersSkills();
        public MemberByIdDto GetMember(long id);
        public List<MemberSkillbyIdDto> GetMemberSkills(long id);
        public List<MemberLanguageByIdDto> GetMemberLanguages(long id);
        public List<MemberFilterDto> Get(long category_id, List<long> skill_id, List<long> skill_level_id);
        public Boolean validateCategoryWithSkills(long category_id, List<long> skill_id);
        public MemberFilterDto InsertSkillIntoMember(long memberId, long skillId, long skillLevelId);
        public bool DeleteSkillsFromMember(long memberId, long skillId);
        public MemberFilterDto UpdateSkillIntoMember(long memberId, long skillId, long skillLevelId);

        public void Update(Member members);
        public void Add(Member members);
        public void Delete(long id);

    }
}