using Mvc_Birkan.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc_Birkan.Controllers
{
    public class KullaniciController : Controller
    {
        //
        // GET: /Kullanici/
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            
            return View(users);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanılmış Mail Adresi Girildi";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmış Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Geçersiz Gizli Cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Geçersiz Mail Adresi";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Geçersiz Parola";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Geçersiz Kullanıcı Key Hatası";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Geçersiz Gizli Soru";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Geçersiz Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Üye Yönetimi Sağlayıcısı Hatası";
                    break;
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı Engel Hatası";
                    break;
                default:
                    break;
            }

            ViewBag.Mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }
        [HttpPost]
        public ActionResult RolAta (string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UyeRolleri(string kullaniciAdi)
        {
            List<string> roller = Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";
            }
            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }
	}
}