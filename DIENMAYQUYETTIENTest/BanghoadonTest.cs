using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIENMAYQUYETTIEN.Areas.Admin.Controllers;
using System.Web.Mvc;
using Moq;

namespace DIENMAYQUYETTIENTest
{
    [TestClass]
    public class BanghoadonTest
    {
        [TestMethod]
        public void TestDetails()
        {
            var controller = new ProductAdminController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server.MapPath("~/App_Data/0")).Returns("~/App_Data/0");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Details("0") as FilePathResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("images", result.ContentType);
            Assert.AreEqual("~/App_Data/0", result.FileName);
        }
        [TestMethod]
        public void TestIndex()
        {
            var controller = new ProductAdminController();
            var result = controller.Index() as ViewResult;
            var db = new CS4PEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Bangsapnham>));
            Assert.AreEqual(db.BangSanPhams.Count(), ((List<BangSanPham>)result.Model).Count);
        }

        [TestMethod]
        public void UpdateTest()
        {

        }

        [TestMethod]
        public void AddTest()
        {

        }


        [TestMethod]
        public void DeleteTest()
        {

        }

        [TestMethod]
        public void LoginTest()
        {

        }

        [TestMethod]
        public void LogoutTest()
        {

        }

    }
}
