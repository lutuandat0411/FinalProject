using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DIENMAYQUYETTIEN.Models;
using System.Web.Mvc;

namespace DIENMAYQUYETTIEN.Controllers
{
    public class HomeController : Controller
    {
        DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();
        public ActionResult Index()
        {
            var product = db.Products.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        public FileResult LoadImage(String id)
        {
            var path = Server.MapPath("~/Image/" + id);
            return File(path, "Imagefile");
        }
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Install()
        {

            return View();
        }
        public ActionResult News()
        {

            return View();
        }
    }
}