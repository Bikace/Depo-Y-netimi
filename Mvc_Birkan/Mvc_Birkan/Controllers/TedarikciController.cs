using Mvc_Birkan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Birkan.Controllers
{
    [Authorize]
    public class TedarikciController : Controller
    {
        
        //
        // GET: /Tedarikci/
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            List<Tedarikciler> data = ctx.Tedarikcilers.ToList();
            return View(data);
        }
        [HttpPost]
        public string Sil(int id)
        {
            Tedarikciler t = ctx.Tedarikcilers.FirstOrDefault(x => x.TedarikciID == id);
            ctx.Tedarikcilers.Remove(t);
            try
            {
                ctx.SaveChanges();
                return "başarılı";
            }
            catch (Exception)
            {
                return "hata";
            }

        }
	}
}