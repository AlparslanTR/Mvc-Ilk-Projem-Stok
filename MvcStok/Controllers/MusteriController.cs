using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        StokEntities db = new StokEntities();
        public ActionResult Index(string arama,int sayfa=1) // Müşteri Listeleme
        {
            //var musteri= db.Musteriler.ToList(); //Tek Sayfada Listeler
            if (!string.IsNullOrEmpty(arama))
            {
                var Sonuc1 = db.Musteriler.ToList().Where(m => m.AdSoyad.Contains(arama)).ToPagedList(sayfa, 10);
                return View(Sonuc1);
            }
            var Sonuc2 = db.Musteriler.ToList().ToPagedList(sayfa, 10);
            return View(Sonuc2);
        
    }
        [HttpGet]
        public ActionResult YeniMusteri() // Eğer ekleme yapmazsa sayfayı boş döndür
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Musteriler p1) // Müşteri Ekleme
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Musteriler.Add(p1);
            db.SaveChanges();
            Response.Write("<script>alert('Müşteri Eklenmiştir.');</script>");
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var musteriler = db.Musteriler.Find(id);
            db.Musteriler.Remove(musteriler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteriler = db.Musteriler.Find(id);
            return View("MusteriGetir",musteriler);
        }
        public ActionResult Guncelle(Musteriler p1)
        {
            var musteri = db.Musteriler.Find(p1.MusteriID);
            musteri.AdSoyad = p1.AdSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}