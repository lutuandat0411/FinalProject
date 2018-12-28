using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN.Areas.Admin.Models;
using DIENMAYQUYETTIEN.Models;

namespace DIENMAYQUYETTIEN.Areas.Admin.Controllers
{
    public class InstallmentBillController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: /Admin/InstallmentBill/
        public ActionResult Index()
        {
            var installmentbills = db.InstallmentBills.Include(i => i.Customer);
            if (Session["UserName"] != null)
            {
                return View(installmentbills);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        // GET: /Admin/InstallmentBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentbill = db.InstallmentBills.Find(id);
            if (installmentbill == null)
            {
                return HttpNotFound();
            }
            return View(installmentbill);
        }

        // GET: /Admin/InstallmentBill/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode");
            return View();
        }

        // POST: /Admin/InstallmentBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentbill)
        {
            if (ModelState.IsValid)
            {
                db.InstallmentBills.Add(installmentbill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentbill.CustomerID);
            return View(installmentbill);
        }

        // GET: /Admin/InstallmentBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentbill = db.InstallmentBills.Find(id);
            if (installmentbill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentbill.CustomerID);
            return View(installmentbill);
        }

        // POST: /Admin/InstallmentBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentbill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentbill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentbill.CustomerID);
            return View(installmentbill);
        }

        // GET: /Admin/InstallmentBill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentbill = db.InstallmentBills.Find(id);
            if (installmentbill == null)
            {
                return HttpNotFound();
            }
            return View(installmentbill);
        }
        private void checkInstallBill(InstallmentBill inBill)
        {
            if (inBill.Method == null || inBill.Method.Equals(""))
                ModelState.AddModelError("Method", "Không được trống");
            if (inBill.Shipper == null || inBill.Shipper.Equals(""))
                ModelState.AddModelError("Shipper", "Vui Lòng Điền Tên Người Giao Hàng");
            if (inBill.Period < 15)
                ModelState.AddModelError("Period", "Thời gian góp vui lòng từ 15 ngày trở lên");
            if (inBill.Taken <= 0)
                ModelState.AddModelError("Taken", "Tiền đã nhận vui lòng lớn hơn 0");
            if (inBill.Remain <= 0)
                ModelState.AddModelError("Remain", "Tiền phải góp vui lòng lớn hơn 0");
        }
        public ActionResult PrintBill(int id)
        {
            var order = db.InstallmentBills.FirstOrDefault(o => o.ID == id);
            if (order != null)
            {
                InstallReceipt rp = new InstallReceipt();
                rp.BillCode = order.BillCode;
                rp.Customer.CustomerName = order.Customer.CustomerName;
                rp.Shipper = order.Shipper;
                rp.Method = order.Method;
                rp.Date = order.Date;
                rp.Period = order.Period;
                rp.Note = order.Note;
                rp.Taken = order.Taken;
                rp.Remain = order.Remain;

                rp.InstallmentBillDetail = order.InstallmentBillDetails.ToList();

                return View(rp);
            }
            return View();
        }
        // POST: /Admin/InstallmentBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBill installmentbill = db.InstallmentBills.Find(id);
            db.InstallmentBills.Remove(installmentbill);
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
