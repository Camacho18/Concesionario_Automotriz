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
                                       Precio = A.PrecioVenta.ToString(),
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
                    //db.SP_Automovil_Create_Fabrica(model.IdFabrica, model.NumeroF, IdUsuario, model.NumeroA, model.IdAnio, model.IdAutoModelo, model.IdAutoColor, model.PrecioCompra, model.PrecioVenta, model.FechaIngreso, Bandera);
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
                                Estado=(from E in db.AutoEstadoList where E.IdAutoEstado==A.IdAutoEstado select E.Nombre).FirstOrDefault()
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

                    Aut.PrecioCompra = model.PrecioCompra;
                    Aut.PrecioVenta = model.PrecioVenta;
                    db.SaveChanges();

                    return Json(new { value = "1", Id=Comodin }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
