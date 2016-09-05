using GeographyAPI.SqlServer.EF;
using GeographyAPI.SqlServer.QueryManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyAPI.Test.SqlServerTests
{
    [TestClass]
    public class CityQueriesTest
    {
        static List<City> cities = new List<City>();
        CityQueries cityQueries;

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
        public async Task Get_All_Continents()
        {
            //Arrange
            cityQueries = new CityQueries(context.Object);

            //Act
            var cqCities = await cityQueries.GetAllCitiesAsync();

            //Assert
            Assert.AreEqual(cities.Count, cqCities.Count);
        }

        [TestMethod]
        public async Task Get_State_By_Id()
        {
            //Arrange
            int cityId = 2;
            cityQueries = new CityQueries(context.Object);

            //Act
            var result = await cityQueries.GetCityAsync(cityId);
            var state = cities.Where(w => w.CityId == cityId).FirstOrDefault();

            //Assert
            Assert.AreEqual(state.CityId, result.CityId);
            Assert.AreEqual(state.Name, result.Name);
        }

        [TestMethod]
        public async Task Get_City_By_Name()
        {
            //Arrange
            string cityName = "Pie";
            cityQueries = new CityQueries(context.Object);

            //Act
            var result = await cityQueries.GetCityAsync(cityName);
            var state = cities.Where(w => w.Name == cityName).FirstOrDefault();

            //Assert
            Assert.AreEqual(state.StateId, result.StateId);
            Assert.AreEqual(state.Name, result.Name);
        }

        [TestMethod]
        public async Task City_Not_Found()
        {
            //Arrange
            string cityName = "Muffin";
            cityQueries = new CityQueries(context.Object);

            //Act
            var result = await cityQueries.GetCityAsync(cityName);

            //Assert
            Assert.IsNull(result);
        }

    }
}
