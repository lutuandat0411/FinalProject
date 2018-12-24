using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIENMAYQUYETTIEN.Areas.Admin.Controllers;
using System.Web.Mvc;
using Moq;
using DIENMAYQUYETTIEN.Models;
using System.Transactions;

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
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Products, ((List<Product>)result.Model).Count);
        }
        
        [TestMethod]
        public void UpdateTest()
        {

        }

        [TestMethod]
        public void AddTest()
        {
            var controller = new ProductAdminController();
            var result = controller.Add() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["Loai_id"], typeof(SelectList));

        }

       
        [TestMethod]
        public void DeleteTest()
        {
            ProductAdminController target = new ProductAdminController();
            //int id = 50;

            var db = new DIENMAYQUYETTIENEntities();

            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    ProductCode = "Code",
                    ProductName = "ProductName",
                    ProductTypeID = db.ProductTypes.First().ID,
                    SalePrice = 123,
                    OriginPrice = 123,
                    InstallmentPrice = 123,
                    Quantity = 123,
                    Avatar = ""
                };
                db.Products.Add(product);
                db.SaveChanges();
                // test view delete
                var result1 = target.Delete(product.ID) as ViewResult;
                Assert.IsNotNull(result1);
                Assert.AreEqual(product.ID, (result1.Model as Product).ID);

                // test delete post
                var count = db.Products.Count();
                var result2 = target.De(product.ID) as RedirectToRouteResult;
                Assert.IsNotNull(result2);
                Assert.AreEqual(count - 1, db.Products.Count());
            }
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
