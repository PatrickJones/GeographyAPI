using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeographyAPI.Controllers;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace GeographyAPI.Test
{
    [TestClass]
    public class ValuesTest
    {
        [TestMethod]
        public void GetValueStrings()
        {
            //Arrange
            var controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var result = controller.Get();

            //Assert
            Array ar;
            Assert.IsTrue(result.TryGetContentValue<Array>(out ar));
            Assert.AreEqual("value1", ar.GetValue(0));
            Assert.AreEqual("value2", ar.GetValue(1));
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void GetInputValue()
        {
            //Arrange
            var controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var param = 5;

            //Act
            var result = controller.Get(param);

            //Assert
            string str;
            Assert.IsTrue(result.TryGetContentValue<string>(out str));
            Assert.AreEqual($"value:{param}", str);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void PostValue()
        {
            //Arrange
            var controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var param = "value test";

            //Act
            var result = controller.Post(param);

            //Assert
            string str;
            Assert.IsTrue(result.TryGetContentValue<string>(out str));
            Assert.AreEqual(param, str);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }

        [TestMethod]
        public void PutValue()
        {
            //Arrange
            var controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var idParam = 3;
            var param = "value test";

            //Act
            var result = controller.Put(idParam, param);

            //Assert
            int id;
            Assert.IsTrue(result.TryGetContentValue<int>(out id));
            Assert.AreEqual(idParam, id);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void DeleteValue()
        {
            //Arrange
            var controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var idParam = 7;

            //Act
            var result = controller.Delete(idParam);

            //Assert
            int id;
            Assert.IsTrue(result.TryGetContentValue<int>(out id));
            Assert.AreEqual(idParam, id);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
