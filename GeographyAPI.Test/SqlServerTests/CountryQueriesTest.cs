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
    public class CountryQueriesTest
    {
        static List<Country> countries = new List<Country>();
        static List<State> states = new List<State>();
        CountryQueries conQueries;

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

            states = new List<State> {
                new State() { StateId = 1, Name = "Water", StateCode = "WA" },
                new State() { StateId = 2, Name = "Fire", StateCode = "FI" },
                new State() { StateId = 3, Name = "Wind", StateCode = "WI" }
            };

            set.SetupData(countries);
            context.Setup(c => c.Countries).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_All_Countries()
        {
            //Arrange
            conQueries = new CountryQueries(context.Object);

            //Act
            var cqCountries = await conQueries.GetAllCountriesAsync();

            //Assert
            Assert.AreEqual(countries.Count, cqCountries.Count);
        }

        [TestMethod]
        public async Task Get_Country_By_Id()
        {
            //Arrange
            int countryId = 2;
            conQueries = new CountryQueries(context.Object);
            
            //Act
            var result = await conQueries.GetCountryAsync(countryId);
            var cont = countries.Where(w => w.CountryId == countryId).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.CountryId, result.CountryId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.CountryCode, result.CountryCode);
        }

        [TestMethod]
        public async Task Get_Country_By_Name()
        {
            //Arrange
            string countryName = "Plain";
            conQueries = new CountryQueries(context.Object);

            //Act
            var result = await conQueries.GetCountryAsync(countryName);
            var cont = countries.Where(w => w.Name == countryName).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.CountryId, result.CountryId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.CountryCode, result.CountryCode);
        }

        [TestMethod]
        public async Task Get_Country_And_States()
        {
            //Arrange
            int countryId = 2;
            var single = countries.Where(w => w.CountryId == countryId).FirstOrDefault();
            single.States.Add(states[0]);
            single.States.Add(states[1]);
            single.States.Add(states[2]);

            conQueries = new CountryQueries(context.Object);

            //Act
            var result = await conQueries.GetCountryAsync(countryId);
            var cont = countries.Where(w => w.CountryId == countryId).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.CountryId, result.CountryId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.CountryCode, result.CountryCode);
            Assert.IsTrue(result.States.Count == 3);
        }

        [TestMethod]
        public async Task Country_Not_Found()
        {
            //Arrange
            string countryName = "Earth";
            conQueries = new CountryQueries(context.Object);

            //Act
            var result = await conQueries.GetCountryAsync(countryName);

            //Assert
            Assert.IsNull(result);
        }
    }
}
