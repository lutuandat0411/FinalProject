using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIENMAYQUYETTIEN.Areas.Admin.Controllers;
using System.Web.Mvc;
using System.Net.Http;
using DIENMAYQUYETTIEN.Models;
using System.Web;
using System.Web.Routing;

namespace DIENMAYQUYETTIENTest
{
    [TestClass]
    public class BanghoadonTest
    {
        [TestMethod]
        public void TestDetails()
        {
            /*
            var controller = new CashBillController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server.MapPath("~/Image/0")).Returns("~/Image/0");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Details("0") as FilePathResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("images", result.ContentType);
            Assert.AreEqual("~/Image/0", reult.FileName);*/
        }
        [TestMethod]
        public void TestIndex()
        {
            var controller = new CashBillController();
            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<CashBill>));
            Assert.AreEqual(db.CashBills, ((List<CashBill>)result.Model).Count);
        }

        [TestMethod]
        public void UpdateTest()
        {

        }

        [TestMethod]
        public void CreateTest()
        {
            var controller = new CashBillController();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["ProductTypeID"], typeof(SelectList));
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