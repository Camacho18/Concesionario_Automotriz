using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Concesionaria.Models;
using Concesionaria.Models.ModelsSupport;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Concesionaria.AutoComplete;

namespace Concesionaria.Controllers
{
    public class ReferenciasController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private string JsonString = string.Empty;
        private int IdSucursal, IdCliente, IdReferencia;
        // GET: Referencias
        public ActionResult Index()
        {
            IdCliente = Convert.ToInt32(Session["IdCliente"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdCliente != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        public ActionResult RefJson()
        {
            IdCliente = Convert.ToInt32(Session["IdCliente"]);
            //ViewBag.Estado = (from c in db.Cliente where c.IdCliente == IdCliente select c.IdEstado_Cliente);
            List<ReferenciaJson> json = (from R in db.Referencias
                                         where R.IdCliente == IdCliente
                                         select new ReferenciaJson
                                         {
                                             IdReferencia = R.IdReferencia,
                                             Numero = R.Numero,
                                             Nombre = R.Nombre,
                                             TelCel = R.TelCel,
                                             IdCliente = R.IdCliente
                                         }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: Referencias/Create
        public ActionResult CreateRef()
        {
            return PartialView("_CreateReferencia");
        }

        [HttpPost]
        public ActionResult CreateRef(ReferenciaCreate model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateReferencia", model);
            }
            else
            {
                Referencias r = new Referencias();
                try
                {
                    r.Numero = model.Numero;
                    r.Nombre = model.Nombre;
                    r.TelCel = model.TelCel;
                    r.IdCliente = Convert.ToInt32(Session["IdCliente"]);
                    db.Referencias.Add(r);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }
       
        // GET: Referencias/Edit/5
        public ActionResult UpdateRef(int IdReferencia)
        {
           
            ReferenciaCreate model = (from R in db.Referencias
                                   where R.IdReferencia == IdReferencia
                                   select new ReferenciaCreate
                                   {
                                       Numero = R.Numero,
                                       Nombre = R.Nombre,                                      
                                       TelCel = R.TelCel,                                      
                                       IdCliente = R.IdCliente
                                   }).FirstOrDefault();
            Session["IdReferencia"] = IdReferencia;
            return PartialView("_UpdateRef", model);
        }

        // POST: Referencias/Edit/5
        [HttpPost]
        public ActionResult UpdateRef(ReferenciaCreate model)
        {
            
            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateRef", model);
            }
            else
            {
                IdReferencia = Convert.ToInt32(Session["IdReferencia"]);
                try
                {
                    Referencias refe = (from R in db.Referencias
                                    where R.IdReferencia == IdReferencia
                                    select R).SingleOrDefault();
                    refe.Numero = model.Numero;
                    refe.Nombre = model.Nombre;
                    refe.TelCel = model.TelCel;
                    refe.IdCliente = Convert.ToInt32(Session["IdCliente"]);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Referencias/Delete/5
        public ActionResult DeleteRef(int IdReferencia)
        {
            Referencias r = db.Referencias.Where(x => x.IdReferencia == IdReferencia).FirstOrDefault();
            db.Referencias.Remove(r);
            db.SaveChanges();
            return Content("Correcto");
        }

        // POST: Referencias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
