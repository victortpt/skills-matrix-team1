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
    public class MembersDomain : BaseRepository, IMembersDomain 
    {
        private readonly IMembersRepository _membersRepository;
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IMemberSkillRepository _memberSkillRepository;
        private readonly ISkillRepository _skillsRepository;
        private readonly ISkillLevelsRepository _skillLevelsRepository;
        private readonly ILanguagesRepository _languagesRespository;

        public MembersDomain() :
           this(new MembersRepository(), new CategoryRepository(), new MemberSkillRepository(), new SkillRepository(), new SkillLevelsRepository(),new LanguagesRepository())
        { }

        public MembersDomain(IMembersRepository memberRepository, ICategoryRepository categoriesRepository, IMemberSkillRepository memberSkillRepository, ISkillRepository skillsRepository, ISkillLevelsRepository skillLevelsRepository, ILanguagesRepository languagesRepository)
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
        public List<Member> Get()
        {
            return _membersRepository.Get().ToList();
        }

        /// <summary>
        /// Gets a list of all the members DB with relevant data.
        /// </summary>
        /// <returns>JSON string with a list of all members with relevant data.</returns>
        public string GetMembersSkills()
        {
            IQueryable<Member> members = _membersRepository.Get().OrderBy(orderQuery => orderQuery.Surname).ThenBy( orderQuery => orderQuery.Name);
            List<MembersDto> members1 = members.Select( x => new MembersDto{ 
                Id= x.Id,
                Name= x.Name,
                Surname= x.Surname,
                Role= x.Role,
                Last_update = x.LastUpdate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Member_languagesDto = x.MemberLanguages.Select( k=> new LanguagesListDto {
                    Name = k.IdLanguageNavigation.Language1,
                    Level = k.IdLanguageLevelNavigation.Level
                }).ToList(),
                Member_skillsDto = x.MemberSkills.Select( j => new SkillsListDto {
                    Name = j.IdSkillNavigation.Skill1,
                    Level = j.IdSkillLevelNavigation.SkillLevel1
                }).ToList()
            }).ToList();
            return JsonSerializer.Serialize(members1);
        }


        /// <summary>
        /// Gets a specific member by id from Members table.
        /// </summary>
        /// <returns>Returns the data from a member by id.</returns>
        public MemberByIdDto GetMember(long id)
        {
            MemberByIdDto memberId = null;
            if(_membersRepository.Get(id).Any()){
                Member memberAux= _membersRepository.Get(id).Single();
                memberId=new MemberByIdDto{
                    Id=memberAux.Id,    
                    Name=memberAux.Name,
                    Surname=memberAux.Surname,
                    Role=memberAux.Role,
                    Email=memberAux.Email,
                    Username=memberAux.Username,
                    Comments=memberAux.Comments,
                    LastUpdate=memberAux.LastUpdate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
                };
            }
            return memberId;
        }

        /// <summary>
        /// Gets a list of skills from a specific member by id.
        /// </summary>
        /// <returns>List of skills from a member by id.</returns>
        public List<MemberSkillbyIdDto> GetMemberSkills(long id)
        {
            IQueryable<Skill> skillsAux= _skillsRepository.Get().Where(sk=>sk.MemberSkills.Any(m=>m.IdMember==id)).OrderBy(ord=>ord.Skill1);
            IQueryable<Member> emptyList = _membersRepository.Get().Where(m => m.MemberSkills.Any());

            List<MemberSkillbyIdDto> skills=null;
            if(skillsAux.Any() || skillsAux.Any()==emptyList.Any()){
                skills= skillsAux.Select(skl=>new MemberSkillbyIdDto
                {
                    ID=skl.Id,
                    Name=skl.Skill1, 
                        Level= skl.MemberSkills.Where(mx=>mx.IdMember==id).Select(sl=>new SkillsLevelsDto{
                        Id=sl.IdSkillLevelNavigation.Id,
                        Name=sl.IdSkillLevelNavigation.SkillLevel1
                        }).ToList(),
                    Category=skl.MemberSkills.Where(mx=>mx.IdMember==id).Select(cat=> new CategoryDto{
                        Category=cat.IdSkillNavigation.IdCategoryNavigation.Category1,
                        CategoryId=cat.IdSkillNavigation.IdCategoryNavigation.Id
                    }).ToList()
                }).ToList();
            }
            return skills;
        }

        /// <summary>
        /// Gets a list of languages from a specific member by id.
        /// </summary>
        /// <returns>List of languages from a member by id.</returns>
        public List<MemberLanguageByIdDto> GetMemberLanguages(long id)
        {
            IQueryable<Language> languageAux = _languagesRespository.GetLanguages().Where(lan=>lan.MemberLanguages.Any(l=>l.IdMember==id)).OrderBy(order=>order.Language1);
            IQueryable<Member> emptyList = _membersRepository.Get().Where(m => m.MemberLanguages.Any());

            List<MemberLanguageByIdDto> languages=null;
            if(languageAux.Any() || languageAux.Any()==emptyList.Any()){
                languages=languageAux.Select(lang=>new MemberLanguageByIdDto
                {
                    ID=lang.Id,
                    Name=lang.Language1,
                    Code=lang.LanguageCode,
                    Level=lang.MemberLanguages.Where(ml=>ml.IdMember==id).Select(lvl=>new LanguageLevelDto{
                        ID=lvl.IdLanguageLevelNavigation.Id,
                        Name=lvl.IdLanguageLevelNavigation.Level
                    }).ToList()                
                }).ToList();
            }
            return languages;
        }


        /// <summary>
        /// Gets a list of members filter by category, skill and skill_level.
        /// </summary>
        /// <returns>List of members that match that parameters</returns>
        public List<MemberFilterDto> Get(long categoryId, List<long> skillId, List<long> skillLevelId)
        {
            List<MemberFilterDto> membersToret=null;
            if (categoryId!=0 && skillId.Count != 0) {
                if(!validateCategoryWithSkills(categoryId,skillId)) {
                    membersToret= CreateMemberFilterDto(categoryId, skillId, skillLevelId);
                }
            } else {
                membersToret= CreateMemberFilterDto(categoryId, skillId, skillLevelId);
            }
            
            return membersToret;
        }


        public MemberFilterDto InsertSkillIntoMember (long memberId, long skillId, long skillLevelId) { 
            bool member = _membersRepository.Get().Any(x=> x.Id == memberId);
            bool skill = _skillsRepository.Get().Any(x=>x.Id == skillId);
            bool skillLevel = _skillLevelsRepository.Get().Any(x=> x.Id == skillLevelId);
            bool alreadyExists = _memberSkillRepository.Get().Any(x=> x.IdMember == memberId && x.IdSkill==skillId );
            MemberFilterDto memberUpdated;

            if (member && skill && skillLevel) {
                if (!alreadyExists) {
                    MemberSkill memberSkill = new MemberSkill {
                        IdMember = memberId,
                        IdSkill = skillId,
                        IdSkillLevel = skillLevelId
                    };
                    _memberSkillRepository.Add(memberSkill);
                    memberUpdated = CreateDto(_membersRepository.Get().Where(x=> x.Id==memberId)).FirstOrDefault();
                } else {
                    memberUpdated = new MemberFilterDto
                    {
                        Name = "error"
                    };
                }
            } else {
                memberUpdated=null;
            }
            return memberUpdated;
        }
        public MemberFilterDto UpdateSkillIntoMember(long memberId, long skillId, long skillLevelId) {
            Member member = _membersRepository.Get(memberId).FirstOrDefault();
            Skill skill = _skillsRepository.Get().Where(x=> x.Id == skillId).FirstOrDefault();
            SkillLevel skillLevel = _skillLevelsRepository.Get().Where(x=> x.Id == skillId).FirstOrDefault();
            MemberFilterDto memberUpdated = null;
            if (member!=null && skill!=null && skillLevel!=null) {
                MemberSkill memberSkill = _memberSkillRepository.Get().Where(ms => ms.IdSkill == skillId & ms.IdMember == memberId & ms.IdSkillLevel==skillLevelId).FirstOrDefault();
                if (memberSkill== null) {
                    memberSkill = _memberSkillRepository.Get().Where(ms => ms.IdSkill == skillId & ms.IdMember == memberId).FirstOrDefault();
                    if (memberSkill!=null) {
                        memberSkill.IdSkillLevel=skillLevelId;
                        _memberSkillRepository.Update(memberSkill);
                        memberUpdated = CreateDto(_membersRepository.Get().Where(x=> x.Id==memberId)).FirstOrDefault();
                    } 
                } else {
                    memberUpdated = new MemberFilterDto {
                        Name = "error"
                    };
                }                   
            }
            return memberUpdated;
        }

        
        public void Update(Member members)
        {

        }
        public void Add(Member members)
        {
            
        }
        public void Delete(long id)
        {
            
        }
        
        private List<MemberFilterDto> CreateDto (IQueryable<Member> member) {
            List<MemberFilterDto> memberDto = member.Select( x=>  new MemberFilterDto {
                Surname = x.Surname,
                Name = x.Name,
                Category = x.MemberSkills.OrderBy(x=> x.IdSkillNavigation.IdCategoryNavigation.Category1).Select( mCategory => new MemberCategoryDto {
                    Name = mCategory.IdSkillNavigation.IdCategoryNavigation.Category1,
                    Id = mCategory.IdSkillNavigation.IdCategoryNavigation.Id,
                    Skill = mCategory.IdMemberNavigation.MemberSkills.OrderBy(x=> x.IdSkillNavigation.Skill1).Where( skill => skill.IdSkillNavigation.IdCategory == mCategory.IdSkillNavigation.IdCategory).
                    Select( mSkills => new MemberSkillDto {
                        Id = mSkills.IdSkillNavigation.Id,
                        Name = mSkills.IdSkillNavigation.Skill1,
                        LevelId = mSkills.IdSkillLevelNavigation.Id,
                        Level = mSkills.IdSkillLevelNavigation.SkillLevel1        
                    }).ToList()
                }).ToList()
            }).ToList();
            return memberDto;
        }

        /// <summary>
        /// Does the query to get all the members that match all of the params and then return a list.
        /// </summary>
        /// <returns>List of members that match that parameters</returns>
        private List<MemberFilterDto> CreateMemberFilterDto (long categoryId, List<long> skillId, List<long> skillLevelId) {
            List<MemberFilterDto> membersToret=null;
            IQueryable<Member> members = _membersRepository.Get();
            members = GetMembersList(categoryId, skillId, skillLevelId, members);


            if(validateCategory(categoryId) && ValidateSkills(skillId) && ValidateSkillLevels(skillLevelId)) {
                membersToret = CreateDto(members);                
                membersToret.Select(x=> {x.Category = x.Category.Distinct().ToList(); return x;}).ToList();
            }
            return membersToret;
        }
        
        /// <summary>
        /// Delete skills from a specific member by id.
        /// </summary>
        /// <param name="memberSkill"></param>
        /// <returns>Boolean</returns>
        public bool DeleteSkillsFromMember(long memberId, long skillId)
        {
            bool deleteResult;
            bool skillsExist = _memberSkillRepository.Get().Any(x=> x.IdMember==memberId & x.IdSkill==skillId);
            
            if (skillsExist) {
                _memberSkillRepository.Delete(memberId, skillId);
                deleteResult = true;
            }
            else {
                deleteResult = false;
            }
            return deleteResult;
        }

        /// <summary>
        /// Validates that the skills belongs to the category.
        /// </summary>
        /// <returns>true if there's any skill that does not belong to the category especified</returns>
        public bool validateCategoryWithSkills(long categoryId, List<long> skillIds)
        {
            return categoryId!=0 ? _skillsRepository.Get().Where(s => skillIds.Contains(s.Id)).Any(x => x.IdCategory != categoryId) : false;
        }

        /// <summary>
        /// Validates that the category exists in the DB.
        /// </summary>
        /// <returns>true if the category exists in the repository context, false if not</returns>
        private Boolean validateCategory(long categoryId)
        {
            return categoryId!=0 ?  _categoriesRepository.GetCategory().Any(x => x.Id == categoryId) :true ;
        }

        /// <summary>
        /// Validates that the skills on the query exists on the DB.
        /// </summary>
        /// <returns>true if all skills exist on the DB, false any of the IDs does not exist</returns>
        private Boolean ValidateSkills(List<long> skillIds)
        {
            return skillIds.Count!=0 ? skillIds.All(Id=> _skillsRepository.Get().Select(x=> x.Id).Contains(Id)) : true;
        }

        /// <summary>
        /// Validates that the skill levels on the query exists on the DB.
        /// </summary>
        /// <returns>true if all skill levels exist on the DB, false any of the IDs does not exist</returns>
        private Boolean ValidateSkillLevels(List<long> skillLevelsIds)
        {
            return skillLevelsIds.Count!=0 ? skillLevelsIds.All(Id=> _skillLevelsRepository.Get().Select(x=> x.Id).Contains(Id)) : true;
        }

        /// <summary>
        /// Validates that the query params are correct.
        /// </summary>
        /// <returns>Validated member's list</returns>
        private IQueryable<Member> GetMembersList(long categoryId, List<long> skillIds, List<long> skillLevelIds, IQueryable<Member> members)
        {
            if (categoryId !=0 && skillLevelIds.Count !=0 && skillIds.Count !=0)
            {
                members = members.Where(m => m.MemberSkills.
                    Any(x => x.IdSkillNavigation.IdCategory == categoryId &
                            skillLevelIds.Contains( (long)x.IdSkillLevel) &
                            skillIds.Contains((long)x.IdSkill)
                    )
                );
            }
            else if (categoryId !=0 && skillLevelIds.Count !=0)
            {
                members = members.Where(m => m.MemberSkills.
                    Any(m => m.IdSkillNavigation.IdCategory == categoryId &
                            skillLevelIds.Contains((long)m.IdSkillLevel)
                    )
                );
            }
            else if (categoryId !=0 && skillIds.Count !=0)
            {
                members = members.Where(m => m.MemberSkills.
                    Any(m => m.IdSkillNavigation.IdCategory == categoryId &
                            skillIds.Contains((long)m.IdSkill)
                    )
                );
            }
            else if (categoryId !=0)
            {
                members = members.Where(m => m.MemberSkills.
                    Any(m => m.IdSkillNavigation.IdCategory == categoryId)
                );
            }
            else if (skillIds.Count !=0 && skillLevelIds.Count !=0)
            {
                members = members.Where(m => m.MemberSkills.
                    Any(m => skillIds.Contains((long)m.IdSkill) &
                        skillLevelIds.Contains((long)m.IdSkillLevel)
                    )
                );
            }
            else if (skillIds.Count !=0){
                members = members.Where(m => m.MemberSkills.
                    Any(m => skillIds.Contains(m.IdSkillNavigation.Id)
                    )
                );
            }
            
            return members.OrderBy(x => x.Surname).ThenBy(x => x.Name);
        }

    }
}