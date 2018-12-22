using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using DIENMAYQUYETTIEN.Controllers;

namespace DIENMAYQUYETTIENTest
{
    [TestClass]
    public class TestHomeController
    {
        [TestMethod]
        public void TestIndex()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

   
    }
}
