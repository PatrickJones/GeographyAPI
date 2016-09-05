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

namespace GeographyAPI.Test.ApiTests
{
    [TestClass]
    public class ContinentTest
    {
        //[TestMethod]
        //public void Continent_Operations()
        //{
        //    var continent = new Continent() { Name = "Lion", ContinentCode = "LI" };
        //    var continents = new List<Continent>();

        //    var mockSet = EntityFrameworkMoqHelper.CreateMockForDbSet<Continent>()
        //        .SetupForQueryOn(continents)
        //        .WithAdd(continents, "ContinentId")
        //        .WithFind(continents, "ContinentId")
        //        .WithRemove(continents);

        //    var mockContext = EntityFrameworkMoqHelper.CreateMockForDbContext<GlobalStandardsEntities, Continent>(mockSet);

        //    var continentService = new ContinentsController(mockContext.Object);

        //    continentService.PostContinent(continent);
        //    Assert.IsTrue(continents.Contains(continent));
        //}

        [TestMethod]
        public void Continent_Post_Test()
        {
            var continents = new List<Continent>();
            using (var ctx = new GlobalStandardsEntities())
            {
                continents.AddRange(ctx.Continents);
            }

            int lastId = continents.OrderBy(o => o.ContinentId).Select(s => s.ContinentId).LastOrDefault();
            lastId = (lastId == 0) ? 1 : ++lastId;

            var continent = new Continent() { ContinentId = lastId, Name = "Lion", ContinentCode = "LI" };

            var set = new Mock<DbSet<Continent>>();
            set.SetupData(continents);


            var context = new Mock<GlobalStandardsEntities>();
            context.Setup(c => c.Continents).Returns(set.Object);

            var controller = new ContinentsController(context.Object);
            var result = (CreatedAtRouteNegotiatedContentResult<Continent>)controller.PostContinent(continent);

            Assert.AreEqual(lastId, result.Content.ContinentId);
        }

    }
}
