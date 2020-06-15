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
    public class AutoPartesController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private readonly RemoveEspace Remove = new RemoveEspace();
        private int IdUsuario, IdSucursal, IdAutopartes;
        // GET: AutoPartes
        public ActionResult Index()
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        public ActionResult AutopartesJson()
        {
            List<AutopartesJson> json = (from ap in db.Autopartes
                                      select new AutopartesJson
                                      {
                                          IdAutopartes = ap.IdAutopartes,
                                          Nombre = ap.Nombre,
                                          Descripcion = ap.Descripcion,
                                          Categoria = (from a in db.CategoriaAutoparte where a.IdCategoriaAutoparte == ap.IdCategoriaAutoparte select a.Categoria).FirstOrDefault(),
                                          Cantidad = ap.Cantidad_Total
                                      }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: AutoPartes/Create
        public ActionResult CreateAutop()
        {
            ViewBag.CategoriaAutopartes = Model.CategoriaAutopartes();
            return PartialView("_CreateAutoparte");
        }

        // POST: AutoPartes/Create
        [HttpPost]
        public ActionResult CreateAutop(AutopartesCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoriaAutopartes = Model.CategoriaAutopartes();
                return PartialView("_CreateAutoparte");
            }
            else
            {
                Autopartes ap = new Autopartes();
                try
                {
                    ap.Nombre = model.Nombre;
                    ap.Descripcion = model.Descripcion;
                    ap.IdCategoriaAutoparte = model.IdCategoriaAutoparte;
                    ap.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]);
                    ap.Cantidad_Total = model.Cantidad;
                    db.Autopartes.Add(ap);
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: AutoPartes/Edit/5
        public ActionResult UpdateAutop(int IdAutopartes)
        {
            ViewBag.CategoriaAutopartes = Model.CategoriaAutopartes();
            AutopartesCreate model = (from ap in db.Autopartes
                                   where ap.IdAutopartes == IdAutopartes
                                   select new AutopartesCreate
                                   {                                       
                                       Nombre = ap.Nombre,
                                       Descripcion = ap.Descripcion,
                                       IdCategoriaAutoparte = ap.IdCategoriaAutoparte,
                                       IdConcesinaria = ap.IdConcesinaria,
                                       Cantidad = ap.Cantidad_Total
                                   }).FirstOrDefault();
            Session["IdAutopartes"] = IdAutopartes;
            return PartialView("_UpdateAutoparte", model);
        }

        // POST: AutoPartes/Edit/5
        [HttpPost]
        public ActionResult UpdateAutop(AutopartesCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoriaAutopartes = Model.CategoriaAutopartes();
                return PartialView("_UpdateCliente", model);
            }
            else
            {
                IdAutopartes = Convert.ToInt32(Session["IdAutopartes"]);
                try
                {
                    Autopartes autop = (from ap in db.Autopartes
                                    where ap.IdAutopartes == IdAutopartes
                                    select ap).SingleOrDefault();
                    autop.Nombre = model.Nombre;
                    autop.Descripcion = model.Descripcion;
                    autop.IdCategoriaAutoparte = model.IdCategoriaAutoparte;
                    autop.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]);
                    autop.Cantidad_Total = model.Cantidad;
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: AutoPartes/Delete/5
        public ActionResult DeleteAutop(int IdAutopartes)
        {
            Autopartes ap = db.Autopartes.Where(x => x.IdAutopartes == IdAutopartes).FirstOrDefault();
            db.Autopartes.Remove(ap);
            db.SaveChanges();
            return Content("Correcto");
        }        
    }
}
