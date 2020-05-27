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
    public class EmpleadoController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado;

        // GET: Empleado
        public ActionResult Index()
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (/*System.Web.HttpContext.Current.User.Identity.IsAuthenticated &&*/ IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");            
        }

        // GET: Empleado/Details/5
        public ActionResult EmpleadoJson()
        {
            List<EmpleadoJson> json = (from emp in db.Empleado select new EmpleadoJson{
            IdEmpleado=emp.IdEmpleado,
            Nombre=emp.Nombre,
            Telefono=emp.Telefono
            }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: Empleado/Create
        public ActionResult CreateEmp()
        {
            ViewBag.TipoEmp = Model.TipoEmpleado();            
            return PartialView("_Create");
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult CreateEmp(EmpleadoCreate model)
        {
            if (!ModelState.IsValid)
            {
                
                ViewBag.TipoEmp = Model.TipoEmpleado();
                return PartialView("_Create",model);
                    }
            else
            {
                Empleado emp = new Empleado();
                try
                {
                    emp.Nombre = model.Nombre;
                    emp.Telefono = model.Telefono;
                    emp.IdTipoEmpleado = model.IdTipoEmpleado;
                    emp.IdConcesinaria = Convert.ToInt32(Session["IdSucursal"]);
                    db.Empleado.Add(emp);
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Empleado/Edit/5
        public ActionResult UpdateEmp(int IdEmpleado)
        {
            ViewBag.TipoEmp = Model.TipoEmpleado();
            EmpleadoCreate model = (from E in db.Empleado where E.IdEmpleado == IdEmpleado
                                    select new EmpleadoCreate
                                    {                                        
                                        Nombre=E.Nombre,
                                        Telefono=E.Telefono,
                                        IdTipoEmpleado=E.IdTipoEmpleado
                                    }
                                    ).FirstOrDefault();
            Session["IdEmpleado"] = IdEmpleado;
            return PartialView("_Update", model);            
        }

        [HttpPost]
        public ActionResult UpdateEmp(EmpleadoCreate model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.TipoEmp = Model.TipoEmpleado();
                return PartialView("_Update", model);
            }
            else
            {
                IdEmpleado = Convert.ToInt32(Session["IdEmpleado"]);
                try
                {
                    Empleado emp = (from E in db.Empleado
                                     where E.IdEmpleado==IdEmpleado
                                     select E).SingleOrDefault();
                    
                    emp.Nombre = model.Nombre;
                    emp.Telefono = model.Telefono;
                    emp.IdTipoEmpleado = model.IdTipoEmpleado;                                        
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
