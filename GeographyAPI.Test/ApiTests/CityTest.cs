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
    public class CityTest
    {
        static List<City> cities = new List<City>();

        static Mock<DbSet<City>> set = new Mock<DbSet<City>>();
        static Mock<GlobalStandardsEntities> context = new Mock<GlobalStandardsEntities>();

        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
            cities = new List<City> {
                new City() { CityId = 1, Name = "Cake" },
                new City() { CityId = 2, Name = "Pie" },
                new City() { CityId = 3, Name = "Donut" }
            };

            set.SetupData(cities);
            context.Setup(c => c.Cities).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_Cities_Test()
        {
            //Arrange
            var controller = new CitiesController(context.Object);
            //Act
            var result = await controller.GetCitiesAsync();
            //Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task Get_City_By_Id_Test()
        {
            //Arrange
            int cityId = 2;
            var controller = new CitiesController(context.Object);
            //Act
            var city = cities.Where(w => w.CityId == cityId).FirstOrDefault();
            OkNegotiatedContentResult<City> result = await controller.GetCityAsync(cityId) as OkNegotiatedContentResult<City>;
            //Assert
            Assert.AreEqual(city.CityId, result.Content.CityId);
            Assert.AreEqual(city.Name, result.Content.Name);
        }

        [TestMethod]
        public async Task Get_City_By_Name()
        {
            //Arrange
            string cityName = "Donut";
            var controller = new CitiesController(context.Object);
            //Act
            var city = cities.Where(w => w.Name == cityName).FirstOrDefault();
            OkNegotiatedContentResult<City> result = await controller.GetCityByNameAsync(cityName) as OkNegotiatedContentResult<City>;
            //Assert
            Assert.AreEqual(city.CityId, result.Content.CityId);
            Assert.AreEqual(city.Name, result.Content.Name);
        }
     }
}
