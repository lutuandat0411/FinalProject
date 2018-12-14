using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN.Models;

namespace DIENMAYQUYETTIEN.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();
        //
        // GET: /Admin/ProductAdmin/
        public ActionResult Index()
        {
            var product = db.Products.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == id);
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product p, int id)
        {
            if (ModelState.IsValid)
            {
                var pro = db.Products.FirstOrDefault(x => x.ID == id);
                pro.ProductCode = p.ProductCode;
                pro.ProductName = p.ProductName;
                pro.SalePrice = p.SalePrice;
                pro.Quantity = p.Quantity;
                pro.OriginPrice = p.OriginPrice;
                pro.InstallmentPrice = p.InstallmentPrice;
                pro.Status = p.Status;
                pro.ProductTypeID = p.ProductTypeID;
                pro.Avatar = p.Avatar;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                var pro = new Product();
                pro.ProductCode = p.ProductCode;
                pro.ProductName = p.ProductName;
                pro.SalePrice = p.SalePrice;
                pro.Quantity = p.Quantity;
                pro.OriginPrice = p.OriginPrice;
                pro.InstallmentPrice = p.InstallmentPrice;
                pro.Status = p.Status;
                pro.ProductTypeID = p.ProductTypeID;
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == id);
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult De( int id)
        {
            var pro = db.Products.FirstOrDefault(x => x.ID == id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account user)
        {
            if (ModelState.IsValid)
            {
                using (DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities())
                {
                    var loguser = db.Accounts.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)).FirstOrDefault();
                    if (loguser != null){
                        Session["UserID"] = loguser.Username.ToString();  
                        Session["UserName"] = loguser.FullName.ToString();  
                        return RedirectToAction("Index");  
                    }
                }
            }
            return View(user);
        }
           public ActionResult UserDashBoard()  
        {  
            if (Session["UserID"] != null)  
            {  
                return View();  
            } else  
            {  
                return RedirectToAction("Login");  
            }  
        }  
    }  
        
}