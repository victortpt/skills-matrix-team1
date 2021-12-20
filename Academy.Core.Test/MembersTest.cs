using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Globalization;
using Academy.Core.Domains;
using Academy.Core.Models;
using Academy.Core.DTO;
using Academy.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Core.Test
{
    [TestClass]
    public class MembersProfileTest
    {                
        //Generte mock objetcs fields
        private Mock<IMembersRepository> _membersRepository; 
        private Mock<ICategoryRepository>  _categoriesRepository; 
        private Mock<ISkillRepository>  _skillsRepository;
        private Mock<ISkillLevelsRepository> _skillLevelsRepository;
        private Mock<ILanguagesRepository> _languagesRespository;
        private Mock<IMemberSkillRepository> _memberSkillRepository;

        //Domain where the functions are to be tested
        private MembersDomain _membersDomain = null;
        private MemberSkillsDomain _memberSkillsDomain = null;
        private int id=1; // input parameter for get functions to test
        private long category_id=1; // input parameter for get functions to test
        private List<long> skill_ids = new List<long>{1};
        private List<long> skill_level_ids = new List<long>{3}; // input parameter for get functions to test

        [TestInitialize]
        public void Initialize()
        {
            //Generate private mock objects
            _membersRepository = new Mock<IMembersRepository>();
            _categoriesRepository = new Mock<ICategoryRepository>();
            _memberSkillRepository = new Mock<IMemberSkillRepository>(); 
            _languagesRespository = new Mock<ILanguagesRepository>();          
            _skillLevelsRepository = new Mock<ISkillLevelsRepository>();
            _skillsRepository = new Mock<ISkillRepository>();

            //Generate members domain object with the mock repository objects
            _membersDomain = new MembersDomain(_membersRepository.Object, _categoriesRepository.Object, _memberSkillRepository.Object, _skillsRepository.Object, _skillLevelsRepository.Object, _languagesRespository.Object );
            _memberSkillsDomain = new MemberSkillsDomain(_membersRepository.Object, _categoriesRepository.Object, _memberSkillRepository.Object, _skillsRepository.Object, _skillLevelsRepository.Object, _languagesRespository.Object );
        }

       
        //Test for GetMemberSkills---Response OK

        [TestMethod]
        public void GetMemberSkillsSuccesfullTest()
        {    //Expect and actual results
            List<MemberSkillbyIdDto> actualSkillsList;   
            List<MemberSkillbyIdDto> expectedSkillsList = new List<MemberSkillbyIdDto>
            {
                new MemberSkillbyIdDto
                {
                    Name="C#",
                    ID=1,
                    Level=new List<SkillsLevelsDto>
                    {
                        new SkillsLevelsDto
                        {
                            Name="A1",
                            Id=1
                        }
                    },
                    Category=new List<CategoryDto>
                    {
                        new CategoryDto
                        {
                            Category="Programming",
                            CategoryId=1
                        }
                    }
                }
            };

            //setup the mock object
                _skillsRepository
                .Setup(it => it.Get())
                .Returns( 
                    new List<Skill>
                    {
                        new Skill
                        {    
                                Id = 1,
                                Skill1 = "C#",
                                MemberSkills=new List<MemberSkill>
                                {
                                    new MemberSkill
                                    {
                                        IdMember=1,
                                        IdSkillLevelNavigation=new SkillLevel
                                        {
                                            Id=1,
                                            SkillLevel1="A1"
                                        },
                                        IdSkillNavigation=new Skill
                                        {
                                            IdCategoryNavigation= new Category
                                            {
                                                  Id=1,
                                                 Category1="Programming" 
                                            }
                                        }
                                    }
                                }
                           }}.AsQueryable());

           actualSkillsList = _membersDomain.GetMemberSkills(id);
           Assert.AreEqual(expectedSkillsList.Count, actualSkillsList.Count);
           Assert.AreEqual(expectedSkillsList[0].Category[0].Category, actualSkillsList[0].Category[0].Category);
        } 

        //Test for GetmemberSkills----Test for empty in case of empty db
        [TestMethod]
        public void GetMemberSkillsNullTest()
        {
            List<MemberSkillbyIdDto> actualSkillsList;   
            List<MemberSkillbyIdDto> expectedSkillsList = new List<MemberSkillbyIdDto>();
            
            _skillsRepository.Setup(it => it.Get())
                .Returns(
                    new List<Skill>{}.AsQueryable()
                );
            actualSkillsList = _membersDomain.GetMemberSkills(id);
            Assert.AreEqual(expectedSkillsList.Count, actualSkillsList.Count);
        }
        
        //Test for GetMemberLanguages----Response OK

        [TestMethod]
        public void GetMemberLanguagesSuccesfullTest()
        {    //Expect and actual results
            List<MemberLanguageByIdDto> actualLanguagesList;   
            List<MemberLanguageByIdDto> expectedLanguagesList = new List<MemberLanguageByIdDto>
            {
                new MemberLanguageByIdDto
                {
                    Name="C#",
                    ID=1,
                    Code="A1",
                    Level=new List<LanguageLevelDto>{
                        new LanguageLevelDto{
                         Name="A1",
                         ID= 1
                        }
                    }
                }
            };

            //setup the mock object
            _languagesRespository
                .Setup(it => it
                    .GetLanguages())
                .Returns( 
                    new List<Language>
                    {
                        new Language
                        {    
                            Id = -1,
                            Language1 = "C#",
                            LanguageCode="A1",
                            MemberLanguages=new List<MemberLanguage>{
                                new MemberLanguage{
                                    IdMember=1,
                                    IdLanguageLevelNavigation =
                                        new LanguageLevel{
                                            Id=1,
                                            Level="A1"      
                                        }
                                    }
                                }
                        }}.AsQueryable());

           actualLanguagesList = _membersDomain.GetMemberLanguages(id);
           Assert.AreEqual(expectedLanguagesList.Count, actualLanguagesList.Count);
          
        }

         //Test for GetmemberLanguages----Test for empty in case of empty db
        [TestMethod]
        public void GetMemberLanguagesNullTest()
        {
            List<MemberLanguageByIdDto> actualLanguagesList;   
            List<MemberLanguageByIdDto> expectedLanguagesList = new List<MemberLanguageByIdDto>();
            
            _languagesRespository.Setup(it => it.GetLanguages())
                .Returns(
                    new List<Language>{}.AsQueryable()
                );
            actualLanguagesList = _membersDomain.GetMemberLanguages(id);
            Assert.AreEqual(expectedLanguagesList.Count, actualLanguagesList.Count);
        }

         // Test for Getmembers method, check for correct date output, return OK
        [TestMethod]
        public void GetMemberSuccesfullTest()
        {    //Expect and actual results
            MemberByIdDto actualMembersList;   
            MemberByIdDto expectedMembersList = new MemberByIdDto
            {
                    Id = 1,
                    Name = "Victor",
                    Surname = "Busto",
                    Role = "Software Engineer",
                    Email = "victor.busto@softwareone.com",
                    Username = "victortrumpet",
                    Comments = "Good kid",
                    LastUpdate =System.DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
            };

            //setup the mock object
            this._membersRepository
                .Setup(it => it
                    .Get(id))
                .Returns( 
                    new List<Member>
                    {
                new Member
                {    //Le digo al mock object lo que me duelvve la función get al acceder a la falsa db
                        Id = 1,
                        Name = "Victor",
                        Surname = "Busto",
                        Role = "Software Engineer",
                        Email = "victor.busto@softwareone.com",
                        Username = "victortrumpet",
                        Comments = "Good kid",
                        LastUpdate = System.DateTime.Now}}.AsQueryable()
                );
          
          //Obviamente el tipo de output de la función tienen que coincidir.---IQuerable
           actualMembersList = _membersDomain.GetMember(id);

            Assert.AreEqual(actualMembersList.LastUpdate,expectedMembersList.LastUpdate);
            Assert.AreEqual(actualMembersList.Id,expectedMembersList.Id);

        }

         // Test for GetMembersFilter method, return OK
        [TestMethod]
        public void GetMembersFilterSuccesfullTest()
        {    //Expect and actual results
            List<MemberFilterDto> actualMembersList;   
            List<MemberFilterDto> expectedMembersList = new List<MemberFilterDto>{
                new MemberFilterDto{
                    Surname = "Busto",
                    Name = "Victor",
                    Category = new List<MemberCategoryDto>{
                        new MemberCategoryDto{
                            Name = "Programming Languages",
                            Id = 1,
                            Skill = new List<MemberSkillDto>{
                                new MemberSkillDto{
                                    Name = "C#",
                                    Id = 1,
                                    Level = "Begginer",
                                    LevelId = 1
                                }
                            }
                        }
                    }
                }
            };

            //setup the mock object
            this._membersRepository
                .Setup(it => it
                    .Get())
                .Returns( 
                    new List<Member>
                    {
                        new Member
                        {    
                            Id = 1,
                            Surname = "Busto",
                            Name = "Victor",
                            MemberSkills = new List<MemberSkill> {
                                new MemberSkill {
                                    Id = 1,
                                    IdMember = 1,
                                    IdSkill = 1,
                                    IdSkillLevel = 3,
                                    IdSkillLevelNavigation = new SkillLevel {
                                        Id = 3,
                                        SkillLevel1="Begginer"
                                    },
                                    IdMemberNavigation = new Member {
                                        Id = 1,
                                        Surname = "Busto",
                                        Name = "Victor",
                                        MemberSkills= new List<MemberSkill> {
                                            new MemberSkill {
                                                Id = 1,
                                                IdMember = 1,
                                                IdSkill = 1,
                                                IdSkillLevel = 3,
                                                IdSkillLevelNavigation = new SkillLevel {
                                                    Id = 3,
                                                    SkillLevel1="Begginer"
                                                },
                                                IdMemberNavigation = new Member {
                                                    Id = 1,
                                                    Surname = "Busto",
                                                    Name = "Victor",
                                                },
                                                IdSkillNavigation = new Skill {
                                                    Id=1,
                                                    IdCategory = 1,
                                                    Skill1 = "C#",
                                                    IdCategoryNavigation = new Category {
                                                        Id=1,
                                                        Category1="Programming" 
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    IdSkillNavigation = new Skill {
                                        Id=1,
                                        IdCategory = 1,
                                        Skill1 = "C#",
                                        IdCategoryNavigation = new Category {
                                            Id=1,
                                            Category1="Programming" 
                                        }
                                    }
                                }
                            }
                       }
                    }.AsQueryable()
                );
                //setup the mock object for categories
                this._categoriesRepository.Setup(it => it
                    .GetCategory())
                    .Returns( 
                        new List<Category> {
                            new Category {
                                Id=1,
                                Category1="Programming"
                            }
                        }.AsQueryable()
                    );
                //setup the mock object for skills
                this._skillsRepository.Setup(it => it
                    .Get())
                    .Returns( 
                        new List<Skill> {
                            new Skill {
                                Id=1,
                                Skill1="C#",
                                IdCategory=1,
                                IdCategoryNavigation = new Category {
                                    Id=1,
                                    Category1="Programming"
                                }
                            }
                        }.AsQueryable()
                    );
                //setup the mock object for skill levels
                this._skillLevelsRepository.Setup(it => it
                    .Get())
                    .Returns( 
                        new List<SkillLevel> {
                            new SkillLevel {
                                Id=3,
                                SkillLevel1="Beginner"
                            }
                        }.AsQueryable()
                    );
          
            //Obviamente el tipo de output de la función tienen que coincidir.---IQuerable
            actualMembersList = _membersDomain.Get(category_id, skill_ids, skill_level_ids);
            Assert.AreEqual(actualMembersList.Count,expectedMembersList.Count);
        }

        [TestMethod]
        public void GetMembersFilterNullTest()
        {
            List<MemberFilterDto> actualMembersList;   
            List<MemberFilterDto> expectedMembersList = new List<MemberFilterDto>();
            
            _membersRepository.Setup(it => it.Get())
                .Returns(
                    new List<Member>{}.AsQueryable()
                );
            actualMembersList = _membersDomain.Get(category_id, skill_ids, skill_level_ids) ?? new List<MemberFilterDto>();
            Assert.AreEqual(expectedMembersList.Count, actualMembersList.Count);
        }

        [TestMethod]    
        public void CheckIfMemberSkillIsUpdated()
        {
            List<MemberSkill> actualMemberSkillList;
            List<MemberSkill> expectedMemberSkillList= new List<MemberSkill>
            {
                new MemberSkill
                {
                    Id=999999,
                    IdMember=1,
                    IdSkill=1,
                    IdSkillLevel=1

                }
            };
            //Setup the mock object
            _memberSkillRepository
            .Setup(it=>it.Get())
            .Returns(
                new List <MemberSkill>
                {
                    new MemberSkill
                    {
                        Id=9999999,
                        IdMember=1,
                        IdSkill=1,
                        IdSkillLevel=2
                    }
                 }.AsQueryable());
            
            actualMemberSkillList = _memberSkillsDomain.Get();

            Assert.AreEqual(actualMemberSkillList,expectedMemberSkillList);
            // bool exists= _memberSkillRepository.Setup(i => i.Update(expectedMemberSkillList));
            // Assert.IsTrue(exists);
        }
    }
}
