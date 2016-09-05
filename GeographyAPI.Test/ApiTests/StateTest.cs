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
    /// <summary>
    /// Summary description for StateTest
    /// </summary>
    [TestClass]
    public class StateTest
    {
        static List<State> states = new List<State>();

        static Mock<DbSet<State>> set = new Mock<DbSet<State>>();
        static Mock<GlobalStandardsEntities> context = new Mock<GlobalStandardsEntities>();

        [ClassInitialize]
        public static void SetMockCollection(TestContext testContext)
        {
            states = new List<State> {
                new State() { StateId = 1, Name = "Water", StateCode = "WA" },
                new State() { StateId = 2, Name = "Fire", StateCode = "FI" },
                new State() { StateId = 3, Name = "Wind", StateCode = "WI" }
            };

            set.SetupData(states);
            context.Setup(c => c.States).Returns(set.Object);
        }

        [TestMethod]
        public async Task Get_State_Test()
        {
            //Arrange
            var controller = new StatesController(context.Object);
            //Act
            var result = await controller.GetStatesAsync();
            //Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task Get_State_By_Id_Test()
        {
            //Arrange
            int stateId = 2;
            var controller = new StatesController(context.Object);
            //Act
            var state = states.Where(w => w.StateId == stateId).FirstOrDefault();
            OkNegotiatedContentResult<State> result = await controller.GetStateAsync(stateId) as OkNegotiatedContentResult<State>;
            //Assert
            Assert.AreEqual(state.CountryId, result.Content.CountryId);
            Assert.AreEqual(state.Name, result.Content.Name);
            Assert.AreEqual(state.StateCode, result.Content.StateCode);
        }
    }
}
