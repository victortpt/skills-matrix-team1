using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Academy.Core.Domains;
using Academy.Core.Models;
using Academy.Core.DTO;
using Academy.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Core.Test
{
    [TestClass]
    public class LanguageLevelsTest
    {
        private Mock<ILanguageLevelRepository> languageLevelRepository = null;
        private LanguageLevelDomain languageLevelDomain = null;

        [TestInitialize]
        public void Initialize()
        {
            this.languageLevelRepository = new Mock<ILanguageLevelRepository>();

            this.languageLevelDomain =
                new LanguageLevelDomain(this.languageLevelRepository.Object);
        }
        [TestMethod]
        public void GetLanguageLevelList_ExpectedCorrectList()
        {
            IList<LanguageLevelDto> actualLanguageLevelList;
            IList<LanguageLevelDto> expectedLanguageLevelList = new List<LanguageLevelDto>
            {
                new LanguageLevelDto
                {
                    ID = 1,
                    Name = "A1"
                }
            };

            this.languageLevelRepository
                .Setup(it => it
                    .GetLanguagesProficiency())
                .Returns(
                    new List<LanguageLevel>
                    {
                        new LanguageLevel
                        {
                            Id = 1,
                            Level = "A1",
                        }
                    }.AsQueryable()
                );

            actualLanguageLevelList = this.languageLevelDomain.GetLanguagesProficiency();

            Assert.AreEqual(expectedLanguageLevelList[0].ID, actualLanguageLevelList[0].ID);
        }

        [TestMethod]
        public void GetLenguajesLevelList_EmptyList()
        {
            IList<LanguageLevelDto> actualLanguageLevelList;
            IList<LanguageLevelDto> expectedLanguageLevelList = new List<LanguageLevelDto>();

            this.languageLevelRepository
                .Setup(it => it
                    .GetLanguagesProficiency())
                .Returns(
                    new List<LanguageLevel>().AsQueryable()
                );

            actualLanguageLevelList = this.languageLevelDomain.GetLanguagesProficiency();

            Assert.AreEqual(expectedLanguageLevelList.Count, actualLanguageLevelList.Count);
        }

    }
}