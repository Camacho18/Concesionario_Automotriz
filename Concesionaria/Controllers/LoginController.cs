using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//User
using System.Web.Security;
using System.Web.Configuration;
using Concesionaria.Models.ModelsSupport;
using Concesionaria.Models;

namespace Concesionaria.Controllers
{
    public class LoginController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acce(Login model)
        {
            var m = (from U in db.Usuario                     
                     where U.NomUsuario == model.Usuario && U.Contrasena == model.Contras
                     select U.IdUsuario
                     ).FirstOrDefault();
            if (m > 0)
            {
                Session["IdUsuario"] = m;
                Session["IdSucursal"] = (from E in db.Empleado
                                         join U in db.Usuario on E.IdEmpleado equals U.IdEmpleado
                                         where U.IdUsuario == m
                                         select E.IdConcesinaria
                                         ).FirstOrDefault();

                FormsAuthentication.SetAuthCookie(Convert.ToString(10), true);
            }
            return Json(m, JsonRequestBehavior.AllowGet);
        }

        // GET: Login/Create
        public ActionResult Deslog()
        {

            FormsAuthentication.SignOut();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);


            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return View("Index");
        }
    }
}
