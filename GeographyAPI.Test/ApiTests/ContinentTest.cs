using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeographyAPI.SqlServer.EF;
using EntityFramework.MoqHelper;
using GeographyAPI.Controllers;
using Moq;
using System.Linq;
using System.Data.Entity;
using System.Web.Http.Results;
using GeographyAPI.SqlServer.QueryManager;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeographyAPI.Test.ApiTests
{
    [TestClass]
    public class ContinentTest
    {
        static List<Continent> continents = new List<Continent>();

        static Mock<DbSet<Continent>> set = new Mock<DbSet<Continent>>();
        static Mock<GlobalStandardsEntities> context = new Mock<GlobalStandardsEntities>();


        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
            continents = new List<Continent> {
                new Continent() { ContinentId = 1, Name = "Lion", ContinentCode = "LI" },
                new Continent() { ContinentId = 2, Name = "Tiger", ContinentCode = "TI" },
                new Continent() { ContinentId = 3, Name = "Cheeta", ContinentCode = "CH" }
            };

            set.SetupData(continents);
            context.Setup(c => c.Continents).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_Continents_Test()
        {
            //Arrange
            var controller = new ContinentsController(context.Object);
            //Act
            var result = await controller.GetContinentsAsync();
            //Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task Get_Continents_By_Id_Test()
        {
            //Arrange
            int continentId = 2;
            var controller = new ContinentsController(context.Object);
            //Act
            var cont = continents.Where(w => w.ContinentId == continentId).FirstOrDefault();
            OkNegotiatedContentResult<Continent> result = await controller.GetContinentAsync(continentId) as OkNegotiatedContentResult<Continent>;
            //Assert
            Assert.AreEqual(cont.ContinentId, result.Content.ContinentId);
            Assert.AreEqual(cont.Name, result.Content.Name);
            Assert.AreEqual(cont.ContinentCode, result.Content.ContinentCode);
        }
    }
}
