﻿using System;
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
        private int IdUsuario, IdSucursal, IdManten_Autopar;
        // GET: Mantenimiento
        public ActionResult Index()
        {
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
        public ActionResult MantenimientoAutopartesJson(int? IdMantenimiento)
        {
            if (IdMantenimiento == null)
                IdMantenimiento = Convert.ToInt32(Session["IdMantenimiento"]);
            else
                Session["IdMantenimiento"] = IdMantenimiento;

            List<ManteAutoJson> json = (from M in db.Manten_Autopar
                                        where M.IdMantenimiento == IdMantenimiento
                                        select new ManteAutoJson
                                            {
                                                IdManten_Autopar = M.IdManten_Autopar,
                                                Precio = M.Precio,
                                                IdMantenimiento = M.IdMantenimiento,
                                                Autopartes = (from a in db.Autopartes where a.IdAutopartes== M.IdAutopartes select a.Nombre).FirstOrDefault(),
                                                CantidadAutopartes = M.Cantidad_Autopartes
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            var num = (from a in db.Automovil join am in db.AutoModelo on a.IdAutoModelo equals am.IdAutoModelo select am.Nombre).FirstOrDefault();
            return Json(new { JsonString, num }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateMantenimientoAutopartes()
        {
            ViewBag.Autopartes = Model.Autopartes();
            return PartialView("_CreateManteAuto");
        }
        [HttpPost]
        public ActionResult CreateMantenimientoAutopartes(ManteAutoCreate model)
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
        public ActionResult UpdateMantenimientoAutopartes(int IdManten_Autopar)
        {
            ViewBag.Autopartes = Model.Autopartes();
            ManteAutoCreate model = (from M in db.Manten_Autopar
                                     where M.IdManten_Autopar == IdManten_Autopar
                                     select new ManteAutoCreate
                                     {
                                         Precio = M.Precio,
                                         IdMantenimiento = M.IdMantenimiento,
                                         IdAutopartes = M.IdAutopartes,
                                         Cantidad_Autopartes = M.Cantidad_Autopartes                                         
                                     }
                                    ).FirstOrDefault();
            Session["IdManten_Autopar"] = IdManten_Autopar;
            return PartialView("_UpdateManteAuto", model);
        }
        [HttpPost]
        public ActionResult UpdateMantenimientoAutopartes(ManteAutoCreate model)
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
                    ma.IdMantenimiento = Convert.ToInt32(Session["IdMantenimiento"]);
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
        public ActionResult DeleteMantenimientoAutoparte(int IdManten_Autopar)
        {
            try
            {
                List<Manten_Autopar> MantAutoModel = (from ma in db.Manten_Autopar select ma).ToList();
                foreach (var MatA in MantAutoModel)
                {
                    List<Mantenimiento> MantModel = (from mt in db.Mantenimiento where mt.IdMantenimiento == MatA.IdMantenimiento select mt).ToList();
                    foreach (var Mant in MantModel)
                    {
                        List<MantenEstado> EstModel = (from e in db.MantenEstado where e.IdMantenEstado == Mant.IdMantenEstado select e).ToList();
                        foreach (var Est in EstModel)
                        {
                            if (Est.Nombre == "En reparación")
                            {
                                db.Manten_Autopar.Remove(MatA);
                                db.SaveChanges();
                                return Json("1", JsonRequestBehavior.AllowGet);
                            }
                            else 
                                return Json("2", JsonRequestBehavior.AllowGet);
                        }
                        return Json("0", JsonRequestBehavior.AllowGet);

                    }
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}