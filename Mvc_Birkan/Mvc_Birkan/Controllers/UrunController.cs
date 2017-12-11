using Mvc_Birkan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Birkan.Controllers
{
    [Authorize]
    public class UrunController : Controller 
    {
        //
        // GET: /Urun/
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            List<Urunler> urunler = ctx.Urunlers.ToList();

            return View(urunler);
        }
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = ctx.Kategorilers.ToList();
            ViewBag.Tedarikciler = ctx.Tedarikcilers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urunler u)
        {
            ctx.Urunlers.Add(u);
            ctx.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult Sil(int id)
        {
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Sil(Urunler u)
        {
            Urunler urn = ctx.Urunlers.FirstOrDefault(x => x.UrunID == u.UrunID);
            ctx.Urunlers.Remove(urn);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = ctx.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(ctx.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.TedarikciID = new SelectList(ctx.Tedarikcilers, "TedarikciID", "SirketAdi", urunler.TedarikciID);
            return View(urunler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,TedarikciID,KategoriID,BirimdekiMiktar,Fiyat,Stok,YeniSatis,EnAzYenidenSatisMikatari,Sonlandi")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                var Entity = ctx.Urunlers.Find(urunler.UrunID);
                if (Entity != null)
                {
                    Entity.UrunAdi = urunler.UrunAdi;
                    Entity.TedarikciID = urunler.TedarikciID;
                    Entity.KategoriID = urunler.KategoriID;
                    Entity.BirimdekiMiktar = urunler.BirimdekiMiktar;
                    Entity.Fiyat = urunler.Fiyat;
                    Entity.Stok = urunler.Stok;
                    Entity.YeniSatis = urunler.YeniSatis;
                    Entity.EnAzYenidenSatisMikatari = urunler.EnAzYenidenSatisMikatari;
                    Entity.Sonlandi = urunler.Sonlandi;
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(urunler);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}