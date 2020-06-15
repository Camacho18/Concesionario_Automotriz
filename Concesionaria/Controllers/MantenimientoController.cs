using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Concesionaria.AutoComplete;
using Concesionaria.Models;
using Concesionaria.Models.ModelsSupport;
using Newtonsoft.Json;

namespace Concesionaria.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        //private readonly RemoveEspace Remove = new RemoveEspace();
        private int IdUsuario, IdSucursal, IdManten_Autopar, IdMantenimiento;
        // GET: Mantenimiento
        public ActionResult Index()
        {
            IdMantenimiento = Convert.ToInt32(Session["IdMantenimiento"]);
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        // GET: Mantenimiento/Details/5
        public ActionResult MantenimientoJson()
        {
            List<MantenimientoJson> json = (from m in db.Mantenimiento
                                         select new MantenimientoJson
                                         {
                                             IdMantenimiento = m.IdMantenimiento,
                                             Fecha = m.Fecha,
                                             Usuario = (from u in db.Usuario where u.IdUsuario==m.IdUsuario select u.NomUsuario).FirstOrDefault(),
                                             Automovil = (from a in db.Automovil join am in db.AutoModelo on a.IdAutoModelo equals am.IdAutoModelo select am.Nombre).FirstOrDefault(),
                                             Estado = (from e in db.MantenEstado where e.IdMantenEstado==m.IdMantenEstado select e.Nombre).FirstOrDefault()
                                         }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: Mantenimiento/Create
        public ActionResult CreateMantenimiento()
        {
            ViewBag.Automovil = Model.Automovil();
            return PartialView("_CreateMantenimiento");
        }

        // POST: Mantenimiento/Create
        [HttpPost]
        public ActionResult CreateMantenimiento(MantenimientoCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Automovil = Model.Automovil();
                return PartialView("_CreateMantenimiento");
            }
            else
            {
                Mantenimiento m = new Mantenimiento();
                try
                {
                    DateTime actual = DateTime.Now.Date;

                    m.Fecha = actual.Date;
                    m.IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    m.IdAutomovil = model.IdAutomovil;
                    m.IdMantenEstado = 1;                    
                    db.Mantenimiento.Add(m);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // Mantenimiento Autopartes
        public ActionResult ManteAutoJson(int? Id)
        {
            if (Id == null)
                Id = Convert.ToInt32(Session["IdMantenimiento"]);
            else
                Session["IdMantenimiento"] = Id;

            List<ManteAutoJson> json = (from m in db.Manten_Autopar
                                        where m.IdMantenimiento == Id
                                            select new ManteAutoJson
                                            {
                                                IdManten_Autopar = m.IdManten_Autopar,
                                                Precio = m.Precio,
                                                Autopartes = (from a in db.Autopartes where a.IdAutopartes==m.IdAutopartes select a.Nombre).FirstOrDefault(),
                                                Cantidad_Autopartes = m.Cantidad_Autopartes
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateManteAuto()
        {
            ViewBag.Autopartes = Model.Autopartes();
            return PartialView("_CreateManteAuto");
        }
        [HttpPost]
        public ActionResult CreateManteAuto(ManteAutoCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Autopartes = Model.Autopartes();
                return PartialView("_CreateManteAuto", model);
            }
            else
            {
                Manten_Autopar m = new Manten_Autopar();                
                try
                {
                    m.Precio = model.Precio;
                    m.IdMantenimiento = Convert.ToInt32(Session["IdMantenimiento"]); 
                    m.IdAutopartes = model.IdAutopartes;
                    m.Cantidad_Autopartes = model.Cantidad_Autopartes;
                    db.Manten_Autopar.Add(m);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateManteAuto(int Id)
        {
            ViewBag.Autopartes = Model.Autopartes();
            ManteAutoCreate model = (from M in db.Manten_Autopar
                                     where M.IdManten_Autopar == Id
                                     select new ManteAutoCreate
                                     {
                                         Precio = M.Precio,
                                         IdMantenimiento = M.IdMantenimiento,
                                         IdAutopartes = M.IdAutopartes,
                                         Cantidad_Autopartes = M.Cantidad_Autopartes                                         
                                     }
                                    ).FirstOrDefault();
            Session["IdManten_Autopar"] = Id;
            return PartialView("_UpdateManteAuto", model);
        }
        [HttpPost]
        public ActionResult UpdateManteAuto(ManteAutoCreate model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Autopartes = Model.Autopartes();
                return PartialView("_UpdateManteAuto", model);
            }
            else
            {
                IdManten_Autopar = Convert.ToInt32(Session["IdManten_Autopar"]);
                try
                {
                    Manten_Autopar ma = (from m in db.Manten_Autopar
                                           where m.IdManten_Autopar == IdManten_Autopar
                                           select m).SingleOrDefault();

                    ma.Precio = model.Precio;
                    ma.IdMantenimiento = Convert.ToInt32(Session["IdManten_Autopar"]);
                    ma.IdAutopartes = model.IdAutopartes;
                    ma.Cantidad_Autopartes = model.Cantidad_Autopartes;
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }
        public ActionResult DeleteManteAuto(int IdManten_Autopar)
        {
            Manten_Autopar m = db.Manten_Autopar.Where(x => x.IdManten_Autopar == IdManten_Autopar).FirstOrDefault();
            db.Manten_Autopar.Remove(m);
            db.SaveChanges();
            return Content("Correcto");
        }


    }
}
