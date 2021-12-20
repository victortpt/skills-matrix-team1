using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Academy.Core.Domains;
using Academy.Core.Models;
using Academy.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Core.Test
{
    [TestClass]
    public class SkillLevelsTest
    {
        private Mock<ISkillLevelsRepository> skillLevelsRepository = null;
        private SkillLevelsDomain skillLevelsDomain = null;

        [TestInitialize]
        public void Initialize()
        {
            this.skillLevelsRepository = new Mock<ISkillLevelsRepository>();

            this.skillLevelsDomain =
                new SkillLevelsDomain(this.skillLevelsRepository.Object);
        }
        [TestMethod]
        public void GetSuccesfullTest()
        {
            IList<SkillLevel> actualskillLevelsList;
            IList<SkillLevel> expectedskillLevelsList = new List<SkillLevel>
            {
                new SkillLevel
                {
                    Id = 1,
                    SkillLevel1 = "Beginner",
                    NumericalSkillLevel = 5
                }
            };

            this.skillLevelsRepository
                .Setup(it => it
                    .Get())
                .Returns(
                    new List<SkillLevel>
                    {
                        new SkillLevel
                        {
                            Id = 1,
                            SkillLevel1 = "Beginner",
                            NumericalSkillLevel = 5
                        }
                    }.AsQueryable()
                );

            actualskillLevelsList = this.skillLevelsDomain.Get();

            Assert.AreEqual(expectedskillLevelsList.Count, actualskillLevelsList.Count);
            Assert.AreEqual(expectedskillLevelsList[0].Id, actualskillLevelsList[0].Id);
        }

        [TestMethod]
        public void GetNullTest()
        {
            IList<SkillLevel> actualSkillLevelsList;
            IList<SkillLevel> expectedskillLevelsList = new List<SkillLevel>();

            this.skillLevelsRepository
                .Setup(it => it
                    .Get())
                .Returns(
                    new List<SkillLevel>().AsQueryable()
                );

            actualSkillLevelsList = this.skillLevelsDomain.Get();

            Assert.AreEqual(expectedskillLevelsList.Count, actualSkillLevelsList.Count);
        }

    }
}
