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
using System.Web;
using System.Web.Routing;

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
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session["UserName"]).Returns("abc");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Products.Count(), ((List<Product>)result.Model).Count);
        }
        
        [TestMethod]
        public void UpdateTest()
        {
            var db = new DIENMAYQUYETTIENEntities();

            var pro = db.Products.First();

            var controller = new ProductAdminController();
            var session = new Mock<HttpSessionStateBase>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            

            // act
            var result = controller.Edit(pro.ID) as ViewResult;
            Assert.IsNotNull(result);
            // Select List theo Mrs Chau;
            //Assert.IsInstanceOfType(result.ViewData["ProductType"], typeof(SelectList));
            // Select List theo Mr Duy;
            Assert.IsInstanceOfType(result.ViewBag.ProductType, typeof(List<ProductType>));
        }
        [TestMethod]
        public void CreateGetTest()
        {
            var controller = new ProductAdminController();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            // Select List theo Mr Duy;
            Assert.IsInstanceOfType(result.ViewBag.ProductType, typeof(List<ProductType>));
            // Select List theo Mrs Chau;
            //Assert.IsInstanceOfType(result.ViewData["ProductType"], typeof(SelectList));
        }
        [TestMethod]
        public void CreatePostTest()
        {
            var controller = new ProductAdminController();
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var files = new Mock<HttpFileCollectionBase>();
            var file = new Mock<HttpPostedFileBase>();
            context.Setup(c => c.Request).Returns(request.Object);
            request.Setup(r => r.Files).Returns(files.Object);
            files.Setup(f => f["Imagefile"]).Returns(file.Object);
            file.Setup(f => f.ContentLength).Returns(1);
            context.Setup(c => c.Server.MapPath("~/Image")).Returns("~/Image");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new DIENMAYQUYETTIENEntities();
            var model = new Product();
            model.ProductTypeID = db.ProductTypes.First().ID;
            model.ProductName = "TenSP";
            model.ProductCode = "TVI0001";
            model.OriginPrice = 123;
            model.SalePrice = 456;
            model.InstallmentPrice = 789;
            model.Quantity = 10;

            using (var scope = new TransactionScope())
            {
                var result0 = controller.Create(model) as RedirectToRouteResult;
                Assert.IsNotNull(result0);
                file.Verify(f => f.SaveAs(It.Is<string>(s => s.StartsWith("~/Image/"))));
                Assert.AreEqual("Index", result0.RouteValues["action"]);

                file.Setup(f => f.ContentLength).Returns(0);
                var result1 = controller.Create(model) as ViewResult;
                Assert.IsNotNull(result1);
                // Select List theo Mr Duy;
                Assert.IsInstanceOfType(result1.ViewBag.ProductType, typeof(List<ProductType>));
            }
            
        }
    
        [TestMethod]
           public void DeleteTest()
           {
                ProductAdminController target = new ProductAdminController();

                var db = new DIENMAYQUYETTIENEntities();

                using (var scope = new TransactionScope())
                {
                    var product = new Product
                    {
                        ProductCode = "Code",
                        ProductName = "ProductName",
                        ProductTypeID = db.ProductTypes.First().ID,
                        SalePrice = 122,
                        OriginPrice = 122,
                        InstallmentPrice = 122,
                        Quantity = 122,
                        Avatar = ""
                    };
                        db.Products.Add(product);
                        db.SaveChanges();
               //test view delete
                        var result1 = target.Delete(product.ID) as ViewResult;
                        Assert.IsNotNull(result1);
                        Assert.AreEqual(product.ID, (result1.Model as Product).ID);

                //test delete post
                        var count = db.Products.Count();
                        var result2 = target.De(product.ID) as RedirectToRouteResult;
                        Assert.IsNotNull(result2);
                        Assert.AreEqual(count - 1, db.Products.Count());
                }
           }   

       
       
    }
}
