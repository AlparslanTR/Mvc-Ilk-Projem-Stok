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
    public class UrunController : Controller
    {
        // GET: Urun
        StokEntities db =new StokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var urun = db.Urunler.ToList().ToPagedList(sayfa, 10);
            return View(urun);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Ad,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urunler p1)
        {
        
            var ktg = db.Kategoriler.Where(m => m.KategoriID == p1.Kategoriler.KategoriID).FirstOrDefault();
            p1.Kategoriler = ktg;
            db.Urunler.Add(p1);
            db.SaveChanges();
            Response.Write("<script>alert('Ürün Eklenmiştir.');</script>"); 
            return RedirectToAction("Index"); // Ürün Eklendiğinde ürünler kısmına yönlendirir.
        }
        public ActionResult Sil(int id)
        {
            var urunler = db.Urunler.Find(id); // Ürün tablosundaki id değişkenleri bul
            db.Urunler.Remove(urunler); // Ürünleri sil
            db.SaveChanges(); // Değişiklikleri Kaydet
            return RedirectToAction("Index"); // İşlem tamamlandığında Index sayfasına geri dön
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.Urunler.Find(id);
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Ad,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(Urunler p1) // Urünleri Güncelleme İşlemi
        {
            var urun = db.Urunler.Find(p1.UrunID);
            urun.Ad = p1.Ad;
            urun.Marka = p1.Marka;
            //urun.Kategori = p1.Kategori;
            var ktg = db.Kategoriler.Where(m => m.KategoriID == p1.Kategoriler.KategoriID).FirstOrDefault();
            urun.Kategoriler = ktg;
            urun.Fiyat = p1.Fiyat;
            urun.Stok = p1.Stok;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}