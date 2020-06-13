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
    public class PromocionController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList DropDownLisObject = new DropDownList();
        private readonly RemoveEspace Remove = new RemoveEspace();

        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdPromocion, IdCom;
        // GET: Promocion

        // [I] PromociónList
        public ActionResult Index()
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        public ActionResult PromocionListJson()
        {
            List<PromocionListJson> json = (from P in db.PromocionList
                                            select new PromocionListJson
                                            {
                                                IdPromocion = P.IdPromocion,
                                                Numero = P.Numero,
                                                Cantidad_Auto = P.Cantidad_Auto,
                                                Descuento = P.Descuento
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: Promocion/Create
        public ActionResult CreatePromList()
        {
            return PartialView("_CreatePromList");
        }

        // POST: Promocion/Create
        [HttpPost]
        public ActionResult CreatePromList(PromocionListCreate model)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("_CreatePromList", model);
            }
            else
            {
                PromocionList modeladd = new PromocionList();
                try
                {
                    model.Numero = Remove.RemoveAllWhitespace(model.Numero);
                    int comd = (from P in db.PromocionList where P.Numero == model.Numero select P.IdPromocion).Count();

                    if (comd >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);

                    modeladd.Numero = model.Numero;
                    modeladd.Cantidad_Auto = model.Cantidad_Auto;
                    modeladd.Descuento = model.Descuento;
                    db.PromocionList.Add(modeladd);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        // [F] PromociónList

        // GET: Empleado/Edit/5   
        public ActionResult PromocionAutoJson(int? Id)
        { 
            if (Id == null)
                Id = Convert.ToInt32(Session["IdPromocion"]);
            else
                Session["IdPromocion"] = Id;

            List<PromocionAutoJson> json = (from PA in db.Promocion_Auto
                                        where PA.IdPromocion_Auto == Id
                                        select new PromocionAutoJson
                                        {
                                            IdPromocion_Auto = PA.IdPromocion_Auto,
                                            AutoModelo = (from M in db.AutoModelo where M.IdAutoModelo == PA.IdAutoModelo select M.Nombre).FirstOrDefault(),
                                            Anios = (from A in db.Anios where A.IdAnios == PA.IdAnios select A.Numero).FirstOrDefault(),
                                            Vigente = (PA.Vigente == true ? "Vigente" : "Caduco")                             
                                        }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            var num = (from P in db.PromocionList where P.IdPromocion == Id select P.Numero).FirstOrDefault();
            return Json(new { JsonString, num }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatePromocionAuto()
        {
            ViewBag.AutoModelo = DropDownLisObject.AutoModelo();
            ViewBag.Anios = DropDownLisObject.Anios();
            return PartialView("_CreatePromAuto");
        }

        [HttpPost]
        public ActionResult CreatePromocionAuto(PromocionAutoCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AutoModelo = DropDownLisObject.AutoModelo();
                ViewBag.Anios = DropDownLisObject.Anios();
                return PartialView("_CreatePromAuto", model);
            }
            else
            {
                Promocion_Auto pa = new Promocion_Auto();
                try
                {
                    pa.IdAutoModelo = model.IdAutoModelo;
                    pa.IdAnios = model.IdAnios;
                    pa.IdPromocion = Convert.ToInt32(Session["IdPromocion"]); ;
                    pa.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]); ;
                    pa.Vigente = true;
                    db.Promocion_Auto.Add(pa);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        // GET: Promocion/Delete/5      

    }
}
