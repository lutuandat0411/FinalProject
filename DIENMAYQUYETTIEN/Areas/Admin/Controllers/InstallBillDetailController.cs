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
    public class InstallBillDetailController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: /Admin/InstallBillDetail/
        public ActionResult Index()
        {
            var installmentbilldetails = db.InstallmentBillDetails.Include(i => i.InstallmentBill).Include(i => i.Product);
            return View(installmentbilldetails.ToList());
        }

        // GET: /Admin/InstallBillDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBillDetail installmentbilldetail = db.InstallmentBillDetails.Find(id);
            if (installmentbilldetail == null)
            {
                return HttpNotFound();
            }
            return View(installmentbilldetail);
        }

        // GET: /Admin/InstallBillDetail/Create
        public ActionResult Create()
        {
            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode");
            return View();
        }

        // POST: /Admin/InstallBillDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BillID,ProductID,Quantity,InstallmentPrice")] InstallmentBillDetail installmentbilldetail)
        {
            if (ModelState.IsValid)
            {
                db.InstallmentBillDetails.Add(installmentbilldetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", installmentbilldetail.ProductID);
            return View(installmentbilldetail);
        }

        // GET: /Admin/InstallBillDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBillDetail installmentbilldetail = db.InstallmentBillDetails.Find(id);
            if (installmentbilldetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", installmentbilldetail.ProductID);
            return View(installmentbilldetail);
        }

        // POST: /Admin/InstallBillDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BillID,ProductID,Quantity,InstallmentPrice")] InstallmentBillDetail installmentbilldetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentbilldetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentbilldetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", installmentbilldetail.ProductID);
            return View(installmentbilldetail);
        }

        // GET: /Admin/InstallBillDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBillDetail installmentbilldetail = db.InstallmentBillDetails.Find(id);
            if (installmentbilldetail == null)
            {
                return HttpNotFound();
            }
            return View(installmentbilldetail);
        }

        // POST: /Admin/InstallBillDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBillDetail installmentbilldetail = db.InstallmentBillDetails.Find(id);
            db.InstallmentBillDetails.Remove(installmentbilldetail);
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
