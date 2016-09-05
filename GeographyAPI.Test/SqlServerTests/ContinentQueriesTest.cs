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
    public class ContinentQueriesTest
    {
        static List<Continent> continents = new List<Continent>();
        static List<Country> countries = new List<Country>();
        ContinentQueries conQueries = new ContinentQueries(new GlobalStandardsEntities());

        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
           continents = new List<Continent> {
                new Continent() { ContinentId = 1, Name = "Lion", ContinentCode = "LI" },
                new Continent() { ContinentId = 2, Name = "Tiger", ContinentCode = "TI" },
                new Continent() { ContinentId = 3, Name = "Cheeta", ContinentCode = "CH" }
            };

            countries = new List<Country> {
                new Country() { Name = "Forest", CountryCode = "FR" },
                new Country() { Name = "Desert", CountryCode = "DS" },
                new Country() { Name = "Plain", CountryCode = "PL" }
            };
        }

        [TestMethod]
        public async Task Get_All_Continents()
        {
            //Arrange
            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);

            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            //Act
            conQueries = new ContinentQueries(context.Object);
            var cqContitnents = await conQueries.GetAllContinentsAsync();

            //Assert
            Assert.AreEqual(continents.Count, cqContitnents.Count);
        }

        [TestMethod]
        public async Task Get_Continent_By_Id()
        {
            //Arrange
            int continentId = 2;

            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);

            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            //Act
            conQueries = new ContinentQueries(context.Object);
            var result = await conQueries.GetContinentAsync(continentId);
            var cont = continents.Where(w => w.ContinentId == continentId).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.ContinentId, result.ContinentId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.ContinentCode, result.ContinentCode);
        }

        [TestMethod]
        public async Task Get_Continent_By_Name()
        {
            //Arrange
            string continentName = "Lion";

            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);

            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            //Act
            conQueries = new ContinentQueries(context.Object);
            var result = await conQueries.GetContinentAsync(continentName);
            var cont = continents.Where(w => w.Name == continentName).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.ContinentId, result.ContinentId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.ContinentCode, result.ContinentCode);
        }

        [TestMethod]
        public async Task Get_Continent_And_Countries()
        {
            //Arrange
            int continentId = 2;
            var single = continents.Where(w => w.ContinentId == continentId).FirstOrDefault();
            single.Countries.Add(countries[0]);
            single.Countries.Add(countries[1]);
            single.Countries.Add(countries[2]);

            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);

            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            //Act
            conQueries = new ContinentQueries(context.Object); 
            var result = await conQueries.GetContinentAsync(continentId);
            var cont = continents.Where(w => w.ContinentId == continentId).FirstOrDefault();

            //Assert
            Assert.AreEqual(cont.ContinentId, result.ContinentId);
            Assert.AreEqual(cont.Name, result.Name);
            Assert.AreEqual(cont.ContinentCode, result.ContinentCode);
            Assert.IsTrue(result.Countries.Count == 3);
        }


        [TestMethod]
        public async Task Continent_Not_Found()
        {
            //Arrange
            string continentName = "Sheep";

            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);

            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            //Act
            conQueries = new ContinentQueries(context.Object);
            var result = await conQueries.GetContinentAsync(continentName);
            var cont = continents.Where(w => w.Name == continentName).FirstOrDefault();

            //Assert
            Assert.IsNull(result);
        }
    }
}
