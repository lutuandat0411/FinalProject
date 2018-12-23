﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIENMAYQUYETTIEN.Areas.Admin.Controllers;
using System.Web.Mvc;
using Moq;

namespace DIENMAYQUYETTIENTest
{
    /// <summary>
    /// Summary description for BangsanphamTest
    /// </summary>
    [TestClass]
    public class BangsanphamTest
    {
     
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
