using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN.Models;
using System.Transactions;

namespace DIENMAYQUYETTIEN.Areas.Admin.Controllers
{
    public class CashBillController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: /Admin/CashBill/
        public ActionResult Index()
        { 
            var cashbill = db.CashBills.OrderByDescending(x => x.ID).ToList();
            if (Session["UserName"] != null)
            {
                return View(cashbill);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: /Admin/CashBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashbill = db.CashBills.Find(id);
            if (cashbill == null)
            {
                return HttpNotFound();
            }
            return View(cashbill);
        }

        // GET: /Admin/CashBill/Create
        public ActionResult Create()
        {
            var cashbill = db.CashBills.OrderByDescending(x => x.ID).ToList();
            return View(Session["CashBill"]);
        }

        // POST: /Admin/CashBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CashBill cashbill)
        {
            checkCashBill(cashbill);
            if (ModelState.IsValid)
            {
                Session["CashBill"] = cashbill;
            }

            return View(cashbill);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2()
        {
            using (var scope = new TransactionScope())
                try
                {
                    var listBill = Session["CashBill"] as CashBill;
                    var billDetail = Session["CashBillDetail"] as List<CashBillDetail>;

                    db.CashBills.Add(listBill);
                    db.SaveChanges();

                    foreach (var detail in billDetail)
                    {
                        detail.BillID = listBill.ID;
                        detail.Product = null;
                        db.CashBillDetails.Add(detail);
                    }
                    db.SaveChanges();
                    scope.Complete();

                    Session["CashBill"] = null;
                    Session["CashBillDetail"] = null;
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            return View("Create");

        }
        private void checkCashBill(CashBill cashBill)
        {
            if (cashBill.CustomerName == null || cashBill.CustomerName.Equals(""))
                ModelState.AddModelError("CustomerName", "Tên khách hàng không được bỏ trống");
            if (cashBill.Address == null || cashBill.Address.Equals(""))
                ModelState.AddModelError("Address", "Địa chỉ không được bỏ trống");
            if (cashBill.PhoneNumber == null || cashBill.PhoneNumber.Equals(""))
                ModelState.AddModelError("PhoneNumber", "Số điện thoại không được bỏ trống");
        }
        // GET: /Admin/CashBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashbill = db.CashBills.Find(id);
            if (cashbill == null)
            {
                return HttpNotFound();
            }
            return View(cashbill);
        }

        // POST: /Admin/CashBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CashBill cashbill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashbill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashbill);
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
