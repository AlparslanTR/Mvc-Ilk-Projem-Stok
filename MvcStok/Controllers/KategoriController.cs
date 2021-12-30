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
    public class KategoriController : Controller
    {
        // GET: Kategori Listeleme
        StokEntities db = new StokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var kategoriler = db.Kategoriler.ToList();
            var kategoriler = db.Kategoriler.ToList().ToPagedList(sayfa, 10);
            return View(kategoriler);
        }
        
        /// ////////////////////////////////////////////////
           
        // Kategori Ekleme
        [HttpGet] // İşlem yoksa sadece sayfayı döndür.
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost] // İşlem varsa kodu çalıştır.
        public ActionResult YeniKategori(Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Kategoriler.Add(p1);
            db.SaveChanges();
            Response.Write("<script>alert('Kategori Eklenmiştir.');</script>");
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id) // Kategori Silme İşlemi
        {
            var kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id) // Güncelleme sayfasına kategorileri gönder
        {
            var ktg = db.Kategoriler.Find(id);
            return View("KategoriGetir", ktg);
        }
        public ActionResult Guncelle(Kategoriler p1)
        {
            var ktg = db.Kategoriler.Find(p1.KategoriID);
            ktg.Ad = p1.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
   
    }
}