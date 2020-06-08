using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//User 
using Concesionaria.Models;
using Concesionaria.Models.ModelsSupport;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Concesionaria.AutoComplete;

namespace Concesionaria.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado;

        // [I] User
        public ActionResult Create(int Id)
        {
            Session["IdEmpleado"] = Id;
            UsuarioModel m = (from U in db.Usuario
                              where U.IdEmpleado == Id
                              select new UsuarioModel
                              {
                                  IdUsuario = U.IdUsuario,
                                  NomUsuario = U.NomUsuario,
                                  Contrasena = U.Contrasena,
                                  Acceso = U.Acceso,
                                  C_Estado=(from E in db.Empleado where E.IdEmpleado==Id select E.C_Estado).FirstOrDefault()
                              }
                     ).FirstOrDefault();

            if (m == null)
            {
                ViewBag.Estado = (from E in db.Empleado where E.IdEmpleado == Id select E.C_Estado).FirstOrDefault();
                return PartialView("_Create");
            }
            else
                return PartialView("_Update", m);
        }
        [HttpPost]
        // GET: Empleado/Edit/5
        public ActionResult Create(UsuarioModel model)
        {
            try
            {
                int comd = (from E in db.Usuario where E.NomUsuario == model.NomUsuario select E.IdUsuario).Count();

                if (comd >= 1)
                    return Json("2", JsonRequestBehavior.AllowGet);

                int id = Convert.ToInt32(Session["IdEmpleado"]);
                Usuario U = new Usuario();
                U.NomUsuario = model.NomUsuario.Trim();
                U.Contrasena = model.Contrasena.Trim();
                U.IdEmpleado = id;
                U.Acceso = true;
                db.Usuario.Add(U);
                db.SaveChanges();


                return Json("1", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        // GET: Empleado/Edit/5
        public ActionResult Update(UsuarioModel model)
        {
         
            int id = Convert.ToInt32(Session["IdEmpleado"]);
            int IdUsu = db.Usuario.Where(x => x.IdEmpleado == id).Select(x => x.IdUsuario).FirstOrDefault();

            Usuario C = db.Usuario.Where(x => x.IdUsuario == IdUsu).FirstOrDefault();
            C.Contrasena = model.Contrasena;
            C.Acceso = model.Acceso;
            db.Entry(C).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        // [F] User
    }
}
