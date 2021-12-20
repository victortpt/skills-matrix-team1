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
    public class CategoriesTest
    {

        //Generte mock objetcs fields
        private Mock<ICategoryRepository>  _categoriesRepository; 
        private Mock<ISkillRepository>  _skillsRepository;

          //Domain where the functions are to be tested
        private CategoryDomain _categoryDomain = null;

    [TestInitialize]
        public void Initialize()
        {
            _skillsRepository = new Mock<ISkillRepository>();
            _categoriesRepository = new Mock<ICategoryRepository>();

            //Generate members domain object with the mock repository objects
            _categoryDomain =
            new CategoryDomain(_categoriesRepository.Object,_skillsRepository.Object);
        }

     [TestMethod]    
        public void CheckIfCategoryExists()
        {
            List<Category> expectedNewCategoryList= new List<Category>
            {
                new Category
                {
                    Id=5,
                    Category1="Programming"
                }
            };
            //Setup the mock object
            _categoriesRepository
            .Setup(it=>it.GetCategory())
            .Returns(
                new List <Category>
                {
                    new Category
                    {
                        Id=5,
                        Category1="Programming"
                    }
                 }.AsQueryable());

        bool exists=_categoryDomain.CheckCagetoryExists(expectedNewCategoryList[0]);
         Assert.IsTrue(exists);
        }

        [TestMethod]    
        public void CheckIfCategoryNotExists()
        {
            List<Category> expectedNewCategoryList= new List<Category>
            {
                new Category
                {
                    Id=5,
                    Category1="Databases"
                }
            };
            //Setup the mock object
            _categoriesRepository
            .Setup(it=>it.GetCategory())
            .Returns(
                new List <Category>
                {
                    new Category
                    {
                        Id=5,
                        Category1="Programming"
                    }
                 }.AsQueryable());

        bool exists=_categoryDomain.CheckCagetoryExists(expectedNewCategoryList[0]);
         Assert.IsFalse(exists);
        }

        
        [TestMethod]    
        public void CheckIfIgnoresUppperLowerAndAccents()
        {
            List<Category> expectedNewCategoryList= new List<Category>
            {
                new Category
                {
                    Id=5,
                    Category1="PróGraMMing"
                }
            };
            //Setup the mock object
            _categoriesRepository
            .Setup(it=>it.GetCategory())
            .Returns(
                new List <Category>
                {
                    new Category
                    {
                        Id=5,
                        Category1="Programming"
                    }
                 }.AsQueryable());

        bool exists=_categoryDomain.CheckCagetoryExists(expectedNewCategoryList[0]);
         Assert.IsTrue(exists);
        }

        [TestMethod]    
        public void CheckIfAssociatedSkillsExists()
        {
            List<Category> expectedCategorySkills = new List<Category>
            {
                new Category
                {
                    Id=5,
                    Category1="New category",
                    
                    Skills=new List<Skill>
                    {
                        new Skill
                        {
                            Id=8,
                            Skill1="New skill",
                            IdCategory=5
                        }

                    }
                }
            };
            _skillsRepository
            .Setup(it=>it.Get())
            .Returns(
                new List<Skill>{

                    new Skill{

                        Id=8,
                        Skill1="Some skill",
                        IdCategory=5
                        
                    }
                 }.AsQueryable());

        bool exists=_categoryDomain.CheckIfAssociatedSkills((int)expectedCategorySkills[0].Id);
        Assert.IsTrue(exists);
        }

        [TestMethod]    
        public void CheckIfAssociatedSkillsDoesNotExists()
        {
            List<Category> expectedCategorySkills = new List<Category>
            {
                new Category
                {
                    Id=10,
                    Category1="New category",
                    
                    Skills=new List<Skill>
                    {
                        new Skill
                        {
                            Id=8,
                            Skill1="New skill",
                            IdCategory=10
                        }

                    }
                }
            };
            _skillsRepository
            .Setup(it=>it.Get())
            .Returns(
                new List<Skill>{

                    new Skill{

                        Id=8,
                        Skill1="Some skill",
                        IdCategory=9
                        
                    }
                 }.AsQueryable());

        bool exists=_categoryDomain.CheckIfAssociatedSkills((int)expectedCategorySkills[0].Id);
        Assert.IsFalse(exists);
        }
    }  
}


