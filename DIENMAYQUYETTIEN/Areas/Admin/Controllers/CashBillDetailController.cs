using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN.Models;

namespace DIENMAYQUYETTIEN.Areas.Admin.Controllers
{
    public class CashBillDetailController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: /Admin/CashBillDetail/
        public ActionResult Index()
        {
            if (Session["CashBillDetail"] == null)
                Session["CashBillDetail"] = new List<CashBillDetail>();
            return View(Session["CashBillDetail"]);
        }

        public int SalePrice(int ProductID)
        {
            return db.Products.Find(ProductID).SalePrice;
        }

        // GET: /Admin/CashBillDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashbilldetail = db.CashBillDetails.Find(id);
            if (cashbilldetail == null)
            {
                return HttpNotFound();
            }
            return View(cashbilldetail);
        }

        // GET: /Admin/CashBillDetail/Create
        public PartialViewResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            var model = new CashBillDetail();
            model.BillID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }

        // POST: /Admin/CashBillDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(CashBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.BillID = Environment.TickCount;
                model.Product = db.Products.Find(model.ProductID);
                var billDetail = Session["CashBillDetail"] as List<CashBillDetail>;
                if (billDetail == null)
                    billDetail = new List<CashBillDetail>();
                billDetail.Add(model);
                Session["CashBillDetail"] = billDetail;
                return RedirectToAction("Create", "CashBill");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", model.ProductID);
            return View("Create", model);
        }

        // GET: /Admin/CashBillDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashbilldetail = db.CashBillDetails.Find(id);
            if (cashbilldetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashbilldetail.ProductID);
            return View(cashbilldetail);
        }

        // POST: /Admin/CashBillDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BillID,ProductID,Quantity,SalePrice")] CashBillDetail cashbilldetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashbilldetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashbilldetail.ProductID);
            return View(cashbilldetail);
        }

        // GET: /Admin/CashBillDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashbilldetail = db.CashBillDetails.Find(id);
            if (cashbilldetail == null)
            {
                return HttpNotFound();
            }
            return View(cashbilldetail);
        }

        // POST: /Admin/CashBillDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBillDetail cashbilldetail = db.CashBillDetails.Find(id);
            db.CashBillDetails.Remove(cashbilldetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
