using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIENMAYQUYETTIEN.Areas.Admin.Controllers;
using System.Web.Mvc;
using System.Net.Http;
using DIENMAYQUYETTIEN.Models;
using System.Web;
using System.Web.Routing;
using Moq;

namespace DIENMAYQUYETTIENTest
{
    [TestClass]
    public class BanghoadonTest
    {
       
        [TestMethod]
        public void TestIndex()
        {
            var controller = new CashBillController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session["UserName"]).Returns("abc");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<CashBill>));
            Assert.AreEqual(db.CashBills.Count(), ((List<CashBill>)result.Model).Count);
        }

        [TestMethod]
        public void UpdateTest()
        {

        }

        [TestMethod]
        public void CreateTest()
        {
            var controller = new CashBillController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session["UserName"]).Returns("abc");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
        }


        


    }
}