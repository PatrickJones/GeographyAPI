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
    public class CountryTest
    {
        static List<Country> countries = new List<Country>();

        static Mock<DbSet<Country>> set = new Mock<DbSet<Country>>();
        static Mock<GlobalStandardsEntities> context = new Mock<GlobalStandardsEntities>();

        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
            countries = new List<Country> {
                new Country() { CountryId = 1, Name = "Forest", CountryCode = "FR" },
                new Country() { CountryId = 2, Name = "Desert", CountryCode = "DS" },
                new Country() { CountryId = 3, Name = "Plain", CountryCode = "PL" }
            };

            set.SetupData(countries);
            context.Setup(c => c.Countries).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_Countries_Test()
        {
            //Arrange
            var controller = new CountriesController(context.Object);
            //Act
            var result = await controller.GetCountriesAsync();
            //Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task Get_Country_By_Id_Test()
        {
            //Arrange
            int countryId = 2;
            var controller = new CountriesController(context.Object);
            //Act
            var cont = countries.Where(w => w.CountryId == countryId).FirstOrDefault();
            OkNegotiatedContentResult<Country> result = await controller.GetCountryAsync(countryId) as OkNegotiatedContentResult<Country>;
            //Assert
            Assert.AreEqual(cont.CountryId, result.Content.CountryId);
            Assert.AreEqual(cont.Name, result.Content.Name);
            Assert.AreEqual(cont.CountryCode, result.Content.CountryCode);
        }

    }
}
