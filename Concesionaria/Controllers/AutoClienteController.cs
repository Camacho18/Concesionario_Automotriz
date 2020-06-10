using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Concesionaria.Models;
using Newtonsoft.Json;
using Concesionaria.Models.ModelsSupport;


namespace Concesionaria.Controllers
{
    public class AutoClienteController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private string JsonString = string.Empty;
        private int IdSucursal, IdCliente;
        // GET: AutoCliente
        public ActionResult Index()
        {
            IdCliente = Convert.ToInt32(Session["IdCliente"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdCliente != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        public ActionResult AutomovilClteJson()
        {
            IdCliente = Convert.ToInt32(Session["IdCliente"]);
            List<AutomovilJson> json = (from V in db.VentaAuto 
                                          join AC in db.AutoCliente on V.IdVentaAuto equals AC.IdVentaAuto
                                          join A in db.Automovil on AC.IdAutomovil equals A.IdAutomovil
                                          join AM in db.AutoModelo on A.IdAutoModelo equals AM.IdAutoModelo
                                          join AE in db.AutoEstadoList on A.IdAutoEstado equals AE.IdAutoEstado
                                          join AMA in db.AutoMarca on AM.IdAutoMarca equals AMA.IdAutoMarca
                                        where V.IdCliente == IdCliente
                                         select new AutomovilJson
                                         {
                                             Numero = A.Numero,
                                             Marca = AMA.Nombre,
                                             Modelo = AM.Nombre,
                                             Estado = AE.Nombre
                                         }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: AutoCliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AutoCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoCliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AutoCliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AutoCliente/Delete/5
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
