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
    public class AccesorioController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList DropDownLisObject = new DropDownList();
        private readonly RemoveEspace Remove = new RemoveEspace();

        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado, IdCom;

        // [I] AccesorioList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccesorioListJson()
        {
            List<AccesorioListJson> json = (from A in db.AccesorioList
                                            orderby A.IdAccesorioList descending
                                            select new AccesorioListJson
                                            {
                                                IdAccesorioList = A.IdAccesorioList,
                                                Numero = A.Numero,
                                                Nombre = A.Nombre,
                                                IdAutoModelo = (from m in db.AutoModelo where m.IdAutoModelo == A.IdAutoModelo select m.Nombre).FirstOrDefault(),
                                                IdAnios = (from m in db.Anios where m.IdAnios == A.IdAnios select m.Numero).FirstOrDefault()
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAcceList()
        {

            ViewBag.AutoModel = DropDownLisObject.AutoModelo();
            ViewBag.Anios = DropDownLisObject.Anios();
            return PartialView("_CreateAcceList");
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult CreateAcceList(AccesorioListCreate model)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("_CreateAcceList", model);
            }
            else
            {
                AccesorioList modeladd = new AccesorioList();
                try
                {
                    model.Numero = Remove.RemoveAllWhitespace(model.Numero);
                    int comd = (from A in db.AccesorioList where A.Numero == model.Numero select A.IdAccesorioList).Count();

                    if (comd >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);

                    modeladd.Numero = model.Numero;
                    modeladd.Nombre = model.Nombre.Trim();
                    modeladd.IdAutoModelo = model.IdAutoModelo;
                    modeladd.IdAnios = model.IdAnios;
                    db.AccesorioList.Add(modeladd);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        // [F] AccesorioList

        // GET: Empleado/Edit/5


        public ActionResult AccesorioJson(int? Id)
        {
            if (Id == null)
                Id = Convert.ToInt32(Session["IdAccesorioList"]);
            else
                Session["IdAccesorioList"] = Id;

            List<AccesorioJson> json = (from A in db.Accesorio where A.IdAccesorioList == Id
                                        orderby A.IdAccesorio descending
                                        select new AccesorioJson
                                        {
                                            IdAccesorio = A.IdAccesorio,
                                            Serie = A.Serie,
                                            Descripcion = A.Descripcion,
                                            Estado = (A.Estado == true ? "Disponible" : "Vendido")
                                        }).ToList();
            JsonString = JsonConvert.SerializeObject(json);

            var num = (from A in db.AccesorioList where A.IdAccesorioList == Id select A.Numero).FirstOrDefault();
            return Json(new { JsonString, num }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAcce()
        {
            return PartialView("_CreateAcce");
        }
        [HttpPost]
        public ActionResult CreateAcce(AccesorioCreate model)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("_CreateAcce", model);
            }
            else
            {
                Accesorio modeladd = new Accesorio();
                try
                {
                    model.Serie = Remove.RemoveAllWhitespace(model.Serie);
                    int comd = (from A in db.Accesorio where A.Serie == model.Serie select A.IdAccesorio).Count();

                    if (comd >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);

                    modeladd.Serie = model.Serie;
                    modeladd.Descripcion = model.Descripcion;
                    modeladd.IdAccesorioList = Convert.ToInt32(Session["IdAccesorioList"]);
                    modeladd.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]);
                    modeladd.Estado = true;
                    db.Accesorio.Add(modeladd);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult UpdateAcce(int Id)
        {

            AccesorioUpdate model = (from E in db.Accesorio
                                     where E.IdAccesorio == Id
                                     select new AccesorioUpdate
                                     {
                                         Serie = E.Serie,
                                         Descripcion = E.Descripcion
                                     }
                                    ).FirstOrDefault();
            Session["IdAccesorio"] = Id;
            return PartialView("_UpdateAcce", model);
        }

        [HttpPost]
        public ActionResult UpdateAcce(AccesorioUpdate model)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("_UpdateAcce", model);
            }
            else
            {
                IdCom = Convert.ToInt32(Session["IdAccesorio"]);
                try
                {
                    Accesorio acc = (from E in db.Accesorio
                                     where E.IdAccesorio == IdCom
                                     select E).SingleOrDefault();


                    acc.Descripcion = model.Descripcion;

                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteAcce(int Id)
        {
            try
            {
                var m = (from A in db.Accesorio where A.IdAccesorio == Id select A).FirstOrDefault();
                if (m.Estado == false)
                    return Json("2", JsonRequestBehavior.AllowGet);
                else
                {
                    db.Accesorio.Remove(m);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        // [I] Auto Accesorio
        public ActionResult AutoAcce()
        {
            IdCom = Convert.ToInt32(Session["IdAuto"]);
            return View("_AutoAcceIndex");
        }

        public ActionResult AccesorioAutoJson()
        {
            IdCom = Convert.ToInt32(Session["IdAuto"]);
            List<AcceAutoJson> json = (from A in db.Accesorio
                                       join AL in db.AccesorioList on A.IdAccesorioList equals AL.IdAccesorioList
                                       join AU in db.AutoAccesorio on A.IdAccesorio equals AU.IdAccesorio
                                       where AU.IdAutomovil ==IdCom
                                            select new AcceAutoJson
                                            {
                                                IdAcceAuto=AU.IdAutoAccesorio,
                                                Serie=A.Serie,
                                                Numero=AL.Numero,
                                                Nombre=AL.Nombre,
                                                Descrip=A.Descripcion
                                            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult CreateAcceAuto()
        {
            IdCom = Convert.ToInt32(Session["IdAuto"]);
            var IdM = (from m in db.Automovil where m.IdAutomovil == IdCom select m.IdAutoModelo).FirstOrDefault();
            var IdA = (from m in db.Automovil where m.IdAutomovil == IdCom select m.IdAnios).FirstOrDefault();

            ViewBag.IdAcce = DropDownLisObject.AutoAcce(IdM, IdA);
            return View("_CreateAcceAuto");
        }
       [HttpPost]
        public ActionResult CreateAcceAuto(CreateAcceAuto Model)
        {
            if (Model.IdAccesorio == 0)
                return Json("2", JsonRequestBehavior.AllowGet);

            else
            {
                IdCom = Convert.ToInt32(Session["IdAuto"]);
                try
                {
                    AutoAccesorio modeladd = new AutoAccesorio();
                    modeladd.IdAccesorio = Model.IdAccesorio;
                    modeladd.IdAutomovil = IdCom;
                    db.AutoAccesorio.Add(modeladd);
                    db.SaveChanges();
                    Accesorio acc = (from E in db.Accesorio
                                     where E.IdAccesorio == Model.IdAccesorio
                                     select E).SingleOrDefault();
                    acc.Estado = false;
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult DeleteAcceAuto(int Id)
        {
            try
            {
                var m = (from A in db.AutoAccesorio where A.IdAutoAccesorio == Id select A).FirstOrDefault();
                Accesorio m2 = (from A in db.Accesorio where A.IdAccesorio == m.IdAccesorio select A).FirstOrDefault();
                m2.Estado = true;
                db.SaveChanges();
                db.AutoAccesorio.Remove(m);
                db.SaveChanges();
                
                
                    return Json("1", JsonRequestBehavior.AllowGet);
                
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        // [F] Auto Accesorio
    }
}
