using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Concesionaria.Models;
using Newtonsoft.Json;
using Concesionaria.Models.ModelsSupport;
using Concesionaria.AutoComplete;


namespace Concesionaria.Controllers
{
    public class AutoClienteController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int  IdVentaAuto;
        // GET: AutoCliente
        public ActionResult Index()
        {
            IdVentaAuto = Convert.ToInt32(Session["IdVentaAuto"]);
            return PartialView();            
        }

        public ActionResult AutoClienteJson()
        {
            IdVentaAuto = Convert.ToInt32(Session["IdVentaAuto"]);
            
            List<AutoClienteJson> json = (from V in db.AutoCliente
                                          where V.IdVentaAuto == IdVentaAuto
                                          select new AutoClienteJson
                                          {
                                              IdAutoCliente = V.IdAutoCliente,
                                              Numero = (from A in db.Automovil where A.IdAutomovil == V.IdAutomovil select A.Numero).FirstOrDefault(),
                                              PrecioVenta = (from A in db.Automovil where A.IdAutomovil == V.IdAutomovil select A.PrecioVenta).FirstOrDefault(),
                                              Marca = (from P in db.Automovil join Ma in db.AutoModelo on P.IdAutoModelo equals Ma.IdAutoModelo join M in db.AutoMarca on Ma.IdAutoMarca equals M.IdAutoMarca where P.IdAutomovil == V.IdAutomovil select M.Nombre).FirstOrDefault(),
                                              Modelo = (from P in db.Automovil join Ma in db.AutoModelo on P.IdAutoModelo equals Ma.IdAutoModelo where P.IdAutomovil == V.IdAutomovil select Ma.Nombre).FirstOrDefault(),
                                              Color = (from P in db.Automovil join C in db.AutoColorList on P.IdAutoColor equals C.IdAutoColor where P.IdAutomovil == V.IdAutomovil select C.Nombre).FirstOrDefault(),
                                              Anio = (from P in db.Automovil join C in db.Anios on P.IdAnios equals C.IdAnios where P.IdAutomovil == V.IdAutomovil select C.Numero).FirstOrDefault()
                                          }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: AutoCliente/Details/5
        public ActionResult CreateAutoCliente()
        {
            ViewBag.Automovil = Model.Automovil();
            return PartialView("_CreateAutoCliente");
        }

        // POST: AutoCliente/Create
        [HttpPost]
        public ActionResult CreateAutoCliente(AutoClienteCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Automovil = Model.Automovil();
                return PartialView("_CreateAutoCliente", model);
            }
            else
            {
                AutoCliente modeladd = new AutoCliente();
                try
                {
                    modeladd.IdAutomovil = model.IdAutomovil;
                    modeladd.IdVentaAuto = Convert.ToInt32(Session["IdVentaAuto"]);
                    db.AutoCliente.Add(modeladd);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        // GET: AutoCliente/Delete/5
        public ActionResult DeleteAuto(int Id)
        {
            try
            {
                var m = (from A in db.AutoCliente where A.IdAutoCliente == Id select A).FirstOrDefault();
                db.AutoCliente.Remove(m);
                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);                
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        //------------------------------ACCESORIOS DE AUTOMOVILES--------------------------
        public ActionResult AutoAccesorioJson(int? IdAutoCliente)
        {
            if (IdAutoCliente == null)
                IdAutoCliente = Convert.ToInt32(Session["IdAutoCliente"]);
            else
                Session["IdAutoCliente"] = IdAutoCliente;

            List<AutoAccesorioJson> json = (from A in db.AutoAccesorio
                                            join AC in db.AutoCliente on A.IdAutomovil equals AC.IdAutomovil
                                            where AC.IdAutoCliente == IdAutoCliente
                                            select new AutoAccesorioJson
                                            {
                                                IdAutoAccesorio = A.IdAutoAccesorio,
                                                Numero = (from AA in db.AutoAccesorio join Ac in db.Accesorio on AA.IdAccesorio equals Ac.IdAccesorio join Al in db.AccesorioList on Ac.IdAccesorioList equals Al.IdAccesorioList where AA.IdAutoAccesorio == A.IdAutoAccesorio select Al.Numero).FirstOrDefault(),
                                                Serie = (from AA in db.AutoAccesorio join Ac in db.Accesorio on AA.IdAccesorio equals Ac.IdAccesorio where AA.IdAutoAccesorio == A.IdAutoAccesorio select Ac.Serie).FirstOrDefault(),
                                                Nombre = (from AA in db.AutoAccesorio join Ac in db.Accesorio on AA.IdAccesorio equals Ac.IdAccesorio join Al in db.AccesorioList on Ac.IdAccesorioList equals Al.IdAccesorioList where AA.IdAutoAccesorio == A.IdAutoAccesorio select Al.Nombre).FirstOrDefault(),
                                                Descripcion = (from AA in db.AutoAccesorio join Ac in db.Accesorio on AA.IdAccesorio equals Ac.IdAccesorio where AA.IdAutoAccesorio == A.IdAutoAccesorio select Ac.Descripcion).FirstOrDefault(),
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            var num = (from A in db.AutoCliente join Ac in db.Automovil on A.IdAutomovil equals Ac.IdAutomovil where A.IdAutoCliente == IdAutoCliente select Ac.Numero).FirstOrDefault();
            return Json(new { JsonString, num }, JsonRequestBehavior.AllowGet);
        }
    }
}
