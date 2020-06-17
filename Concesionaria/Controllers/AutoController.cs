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
using System.Data.Entity.Core.Objects;
using EntityFramework.Extensions;

namespace Concesionaria.Controllers
{
    public class AutoController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList DropDownLisObject = new DropDownList();
        private readonly RemoveEspace RemovesEspecesrr = new RemoveEspace();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, Comodin, ReturnSP;
        // GET: Auto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AutoJson()
        {
            List<AutoJson> json = (from A in db.Automovil
                                   orderby A.IdAutomovil descending
                                   select new AutoJson
                                   {
                                       IdAutomovil = A.IdAutomovil,
                                       Numero = A.Numero,
                                       Adquisicion = "Fábrica",
                                       Precio = A.PrecioTotal.ToString(),
                                       Modelo = (from m in db.AutoModelo where m.IdAutoModelo == A.IdAutoModelo select m.Nombre).FirstOrDefault(),
                                       Estado = (from m in db.AutoEstadoList where m.IdAutoEstado == A.IdAutoEstado select m.Nombre).FirstOrDefault(),
                                   }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: Auto/Details/5
        public ActionResult CreateAuto()
        {
            ViewBag.IdFabrica = DropDownLisObject.Fabrica();
            ViewBag.IdModelo = DropDownLisObject.AutoModelo();
            ViewBag.IdColor = DropDownLisObject.AutoColor();
            ViewBag.IdAnio = DropDownLisObject.Anios();
            return View("_Create");
        }

        [HttpPost]
        public ActionResult CreateAuto(AutomovilCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdFabrica = DropDownLisObject.Fabrica();
                ViewBag.IdModelo = DropDownLisObject.AutoModelo();
                ViewBag.IdColor = DropDownLisObject.AutoColor();
                ViewBag.IdAnio = DropDownLisObject.Anios();
                return View("_Create", model);
            }
            else
            {
                try
                {
                    model.NumeroA = RemovesEspecesrr.RemoveAllWhitespace(model.NumeroA);
                    model.NumeroF = RemovesEspecesrr.RemoveAllWhitespace(model.NumeroF);
                    int comd = (from P in db.Origen_Fabrica where P.Numero == model.NumeroF select P.IdOrigen_Fabrica).Count();
                    int comd2 = (from P in db.Automovil where P.Numero == model.NumeroA select P.IdAutomovil).Count();

                    if (comd >= 1 | comd2 >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);
                    ObjectParameter Bandera = new ObjectParameter("Bandera", typeof(int));
                    IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
                    db.SP_Automovil_Create_Fabrica(model.IdFabrica, model.NumeroF, IdUsuario, model.NumeroA, model.IdAnio, model.IdAutoModelo, model.IdAutoColor, model.PrecioCompra, model.PrecioVenta, model.FechaIngreso, IdSucursal, Bandera);
                    ReturnSP = Convert.ToInt32(Bandera.Value);

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Options(int Id)
        {
            Session["IdAuto"] = Id;
            ViewBag.Num = (from A in db.Automovil where A.IdAutomovil == Id select A.Numero).FirstOrDefault();
            ViewBag.IdEstad = (from A in db.Automovil where A.IdAutomovil == Id select A.IdAutoEstado).FirstOrDefault();

            return View("Edit");
        }

        public ActionResult UpdateFabrica()
        {
            Comodin = Convert.ToInt32(Session["IdAuto"]);
            AutoUpdate m = (from A in db.Automovil
                            join O in db.Origen_Fabrica on A.IdAutomovil equals O.IdAutomovil
                            join F in db.Fabrica on O.IdFabrica equals F.IdFabrica
                            where A.IdAutomovil == Comodin
                            select new AutoUpdate {
                                IdFabrica = F.Numero + " " + F.Nombre,
                                NumeroF = O.Numero,
                                NumeroA = A.Numero,
                                IdAutoModelo = (from mo in db.AutoModelo where mo.IdAutoModelo == A.IdAutoModelo select mo.Nombre).FirstOrDefault(),
                                IdAutoColor = (from c in db.AutoColorList where c.IdAutoColor == A.IdAutoColor select c.Nombre).FirstOrDefault(),
                                IdAnio = (from a in db.Anios where a.IdAnios == A.IdAnios select a.Numero).FirstOrDefault(),
                                FechaIngreso = A.FechaIngreso.ToString(),
                                PrecioCompra = A.PrecioCompra,
                                PrecioVenta = A.PrecioVenta,
                                PrecioTotal = A.PrecioTotal,
                                Estado = (from E in db.AutoEstadoList where E.IdAutoEstado == A.IdAutoEstado select E.Nombre).FirstOrDefault()
                            }
                            ).FirstOrDefault();
            m.IdEstado = 1;
            return View("_Update", m);
        }
        [HttpPost]
        public ActionResult UpdateFabrica(AutoUpdate model)
        {
            if (!ModelState.IsValid)
            {


                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Comodin = Convert.ToInt32(Session["IdAuto"]);
                try
                {
                    Automovil Aut = (from E in db.Automovil
                                     where E.IdAutomovil == Comodin
                                     select E).SingleOrDefault();
                    var p = ((Aut.PrecioTotal - Aut.PrecioVenta) + model.PrecioVenta);
                    Aut.PrecioCompra = model.PrecioCompra;
                    Aut.PrecioVenta = model.PrecioVenta;
                    Aut.PrecioTotal = p;
                    Aut.PrecioPromo = model.PrecioVenta;
                    db.SaveChanges();

                    return Json(new { value = "1", Id = Comodin }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteAuto(int Id)
        {
            try
            {
                Automovil m = (from A in db.Automovil where A.IdAutomovil == Id select A).FirstOrDefault();

                if (m.IdAutoEstado != 1)
                    return Json("2", JsonRequestBehavior.AllowGet);

                List<Accesorio> Ac = (from A in db.Accesorio
                                      join AU in db.AutoAccesorio on A.IdAccesorio equals AU.IdAccesorio
                                      where AU.IdAutomovil == Id
                                      select A).ToList();
                foreach (var acc in Ac)
                {
                    acc.Estado = true;

                }
                db.SaveChanges();
                List<AutoAccesorio> AUC = (from AU in db.AutoAccesorio
                                           where AU.IdAutomovil == Id
                                           select AU).ToList();
                foreach (var auc in AUC)
                {
                    db.AutoAccesorio.Remove(auc);
                }
                db.SaveChanges();

                Automovil AUM = (from A in db.Automovil where A.IdAutomovil == Id select A).FirstOrDefault();
                AUM.IdAutoEstado = 4;
                db.SaveChanges();

                return Json("1", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult TrasAuto(int Id)
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            ViewBag.IdConce = DropDownLisObject.Sucursal(IdSucursal);
            ViewBag.IdAuto = DropDownLisObject.AutoConce(IdSucursal);
            return View("_AutoTras");
        }
        [HttpPost]
        public ActionResult TrasAuto(AutoTraspaso model)
        {
            try
            {
                IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                IdSucursal = Convert.ToInt32(Session["IdSucursal"]);

                // Insert Origen_Traspaso
                Origen_Traspaso OT = new Origen_Traspaso();
                OT.Numero = model.Numero;
                OT.IdVendedor = IdUsuario;
                OT.IdConcesinaria = model.IdConcesionaria;
                OT.IdAutomovil = model.IdAutomovil;
                OT.IdOrigenEstado = 2;
                db.Origen_Traspaso.Add(OT);
                db.SaveChanges();
                // Update Estado del Vehiculo Id=2 - Traspasado
                Automovil AU = (from au in db.Automovil where au.IdAutomovil == model.IdAutomovil select au).FirstOrDefault();
                AU.IdAutoEstado = 2;
                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
