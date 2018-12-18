using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DIENMAYQUYETTIEN.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Data;
using System.Data.Entity;
using System.Web.Security;

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
            if (Session["UserName"] != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public FileResult LoadImage(String id)
        {
            var path = Server.MapPath("~/Image/" + id);
            return File(path, "Imagefile");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, int id)
        {
            CheckProduct(p);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    var path = Server.MapPath("~/Image");
                    path = path + "/" + p.ID;
                    if (Request.Files["Imagefile"] != null &&
                       Request.Files["Imagefile"].ContentLength > 0)
                    {
                        Request.Files["Imagefile"].SaveAs(path);
                    }
                    scope.Complete(); // approve for transaction
                    return RedirectToAction("Index");

                }
            }
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(p);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product p)
        {
            CheckProduct(p);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                    var path = Server.MapPath("~/Image");
                    path = path + "/" + p.ID;
                    if (Request.Files["Imagefile"] != null &&
                        Request.Files["Imagefile"].ContentLength > 0)
                    {
                        Request.Files["Imagefile"].SaveAs(path);

                        scope.Complete(); // approve for transaction
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("Imagefile", "Chưa có hình sản phẩm!");
                }
            }
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(p);
        }
        private void CheckProduct(Product p)
        {
            if (p.OriginPrice < 0 && p.OriginPrice > 100000000)
                ModelState.AddModelError("OriginPrice", "Giá gốc phải lớn hơn 0!");
            if (p.SalePrice < p.OriginPrice && p.SalePrice > 100000000)
                ModelState.AddModelError("SalePrice", "Giá bán phải lớn hơn giá gốc!");
            if (p.InstallmentPrice < p.SalePrice && p.InstallmentPrice > 100000000)
                ModelState.AddModelError("InstallmentPrice", "Giá góp phải lớn hơn giá bán!");
            if (p.ProductName == null && p.ProductName.Length > 100)
                ModelState.AddModelError("ProductName", "Tên sản phẩm không được trống!");
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
        [ValidateAntiForgeryToken]
        public ActionResult De( int id)
        {
            var pro = db.Products.FirstOrDefault(x => x.ID == id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Login()
        {
            if (Session["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
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
                        Session["UserName"] = loguser.FullName.ToString();  
                        return RedirectToAction("Index"); 
                    }
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
              FormsAuthentication.SignOut();
              Session.Abandon();
              return RedirectToAction("Index", "ProductAdmin");
        }
        public ActionResult UserDashBoard()  
        {  
            if (Session["UserName"] != null)  
            {  
                return View();  
            } else  
            {  
                return RedirectToAction("Login");  
            }  
        }  
    }  
        
}