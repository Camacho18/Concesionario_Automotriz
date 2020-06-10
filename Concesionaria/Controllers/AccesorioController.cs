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
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado, Id;

        // GET: Accesorio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccesorioJson()
        {
            List<AccesorioJson> json = (from A in db.Accesorio
                                       orderby A.IdAccesorio descending
                                       select new AccesorioJson
                                       {
                                           IdAccesorio=A.IdAccesorio,
                                           Numero=A.Numero,
                                           Nombre=A.Nombre,
                                           CantidadExistencia=A.CantidadExistencia,
                                           CantidadVendido=A.CantidadVendido                                          
                                       }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAcce()
        {            
            return PartialView("_Create");
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult CreateAcce(AccesorioCreate model)
        {
            if (!ModelState.IsValid)
            {
                
                return PartialView("_Create", model);
            }
            else
            {
                Accesorio modeladd = new Accesorio();
                try
                {
                    int comd = (from A in db.Accesorio where A.Numero == model.Numero select A.IdAccesorio).Count();

                    if (comd >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);

                    modeladd.Numero = model.Numero.Trim();
                    modeladd.Nombre = model.Nombre.Trim();
                    modeladd.CantidadExistencia = model.CantidadExistencia;
                    modeladd.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]);
                    modeladd.CantidadVendido = 0;
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



        // GET: Empleado/Edit/5
        public ActionResult UpdateAcce(int Id)
        {
            
            AccesorioCreate model = (from E in db.Accesorio
                                    where E.IdAccesorio == Id
                                    select new AccesorioCreate
                                    {
                                        Numero = E.Numero,
                                        Nombre = E.Nombre,
                                        CantidadExistencia=E.CantidadExistencia,
                                        CantidadVendido=E.CantidadVendido
                                    }
                                    ).FirstOrDefault();
            Session["IdAccesorio"] = Id;
            return PartialView("_Update", model);
        }

        [HttpPost]
        public ActionResult UpdateAcce(AccesorioCreate model)
        {
            if (!ModelState.IsValid)
            {
                
                return PartialView("_Update", model);
            }
            else
            {
                Id = Convert.ToInt32(Session["IdAccesorio"]);
                try
                {
                    Accesorio emp = (from E in db.Accesorio
                                    where E.IdAccesorio == Id
                                    select E).SingleOrDefault();


                    emp.Nombre = model.Nombre;
                    emp.CantidadExistencia = model.CantidadExistencia;                    
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
}
