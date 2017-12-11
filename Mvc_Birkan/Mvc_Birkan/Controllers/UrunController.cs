using Mvc_Birkan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}