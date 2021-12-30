using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        StokEntities db = new StokEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(Satislar p1)
        {
            db.Satislar.Add(p1);
            db.SaveChanges();
            return View("Index");
        }
    }
}