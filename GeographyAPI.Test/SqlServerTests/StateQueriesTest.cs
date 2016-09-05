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
    public class StateQueriesTest
    {
        static List<State> states = new List<State>();
        static List<City> cities = new List<City>();
        StateQueries stQueries;

        static Mock<DbSet<State>> set = new Mock<DbSet<State>>();
        static Mock<GlobalStandardsEntities> context = new Mock<GlobalStandardsEntities>();

        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
            cities = new List<City> {
                new City() { CityId = 1, Name = "Cake" },
                new City() { CityId = 2, Name = "Pie" },
                new City() { CityId = 3, Name = "Donut" }
            };

            states = new List<State> {
                new State() { StateId = 1, Name = "Water", StateCode = "WA" },
                new State() { StateId = 2, Name = "Fire", StateCode = "FI" },
                new State() { StateId = 3, Name = "Wind", StateCode = "WI" }
            };

            set.SetupData(states);
            context.Setup(c => c.States).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_All_Continents()
        {
            //Arrange
            stQueries = new StateQueries(context.Object);

            //Act
            var cqStates = await stQueries.GetAllStatesAsync();

            //Assert
            Assert.AreEqual(states.Count, cqStates.Count);
        }

        [TestMethod]
        public async Task Get_State_By_Id()
        {
            //Arrange
            int stateId = 2;
            stQueries = new StateQueries(context.Object);

            //Act
            var result = await stQueries.GetStateAsync(stateId);
            var state = states.Where(w => w.StateId == stateId).FirstOrDefault();

            //Assert
            Assert.AreEqual(state.StateId, result.StateId);
            Assert.AreEqual(state.Name, result.Name);
            Assert.AreEqual(state.StateCode, result.StateCode);
        }

        [TestMethod]
        public async Task Get_State_By_Name()
        {
            //Arrange
            string stateName = "Fire";
            stQueries = new StateQueries(context.Object);

            //Act
            var result = await stQueries.GetStateAsync(stateName);
            var state = states.Where(w => w.Name == stateName).FirstOrDefault();

            //Assert
            Assert.AreEqual(state.StateId, result.StateId);
            Assert.AreEqual(state.Name, result.Name);
            Assert.AreEqual(state.StateCode, result.StateCode);
        }

        [TestMethod]
        public async Task Get_State_And_Cities()
        {
            //Arrange
            int stateId = 2;
            var single = states.Where(w => w.StateId == stateId).FirstOrDefault();
            single.Cities.Add(cities[0]);
            single.Cities.Add(cities[1]);
            single.Cities.Add(cities[2]);

            stQueries = new StateQueries(context.Object);

            //Act
            var result = await stQueries.GetStateAsync(stateId);
            var state = states.Where(w => w.StateId == stateId).FirstOrDefault();

            //Assert
            Assert.AreEqual(state.StateId, result.StateId);
            Assert.AreEqual(state.Name, result.Name);
            Assert.AreEqual(state.StateCode, result.StateCode);
            Assert.IsTrue(result.Cities.Count == 3);
        }

        [TestMethod]
        public async Task State_Not_Found()
        {
            //Arrange
            string stateName = "River";
            stQueries = new StateQueries(context.Object);

            //Act
            var result = await stQueries.GetStateAsync(stateName);

            //Assert
            Assert.IsNull(result);
        }

    }
}
