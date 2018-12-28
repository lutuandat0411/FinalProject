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
using DIENMAYQUYETTIEN.Areas.Admin.Models;

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
                return RedirectToAction("~/ProductAdmin/Login");
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
            return View();
        }

        // POST: /Admin/CashBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CashBill cashbill)
        {
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
                    listBill.Date = DateTime.Now;
           
                    db.CashBills.Add(listBill);
                    db.SaveChanges();

                    foreach (var detail in billDetail)
                    {
                        detail.BillID = listBill.ID;
                        detail.Product = null;
                        db.CashBillDetails.Add(detail);
                        listBill.GrandTotal += (detail.Quantity * detail.SalePrice);
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
                try
                {
                    db.Entry(cashbill).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(cashbill);
        }
        public ActionResult PrintCashBill(int id)
        {
            var order = db.CashBills.FirstOrDefault(o => o.ID == id);
            if (order != null)
            {
                ReceiptsModel rp = new ReceiptsModel();
                rp.BillCode = order.BillCode;
                rp.CustomerName = order.CustomerName;
                rp.PhoneNumber = order.PhoneNumber;
                rp.Address = order.Address;
                rp.Date = order.Date;
                rp.Shipper = order.Shipper;
                rp.Note = order.Note;
                rp.GrandTotal = order.GrandTotal;

                rp.CashBillDetail = order.CashBillDetails.ToList();

                return View(rp);
            }
            return View();
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
