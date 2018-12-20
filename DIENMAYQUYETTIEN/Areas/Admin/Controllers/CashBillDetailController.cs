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
            var cashbilldetails = db.CashBillDetails.Include(c => c.CashBill).Include(c => c.Product);
            return View(cashbilldetails.ToList());
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
        public ActionResult Create()
        {
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode");
            return View();
        }

        // POST: /Admin/CashBillDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BillID,ProductID,Quantity,SalePrice")] CashBillDetail cashbilldetail)
        {
            if (ModelState.IsValid)
            {
                db.CashBillDetails.Add(cashbilldetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashbilldetail.ProductID);
            return View(cashbilldetail);
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
