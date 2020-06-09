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
    public class ClienteController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdCliente;
        // GET: Cliente
        public ActionResult Index()
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        public ActionResult ClienteJson()
        {
            List<ClienteJson> json = (from clte in db.Cliente /*join m in db.Municipio on clte.IdMunicipio equals m.IdMunicipio*/
                                      select new ClienteJson
                                      {
                                          IdCliente = clte.IdCliente,
                                          Nombre = clte.Nombre,
                                          Edad = clte.Edad,
                                          Direccion = clte.Direccion,
                                          FechaNac = clte.FechaNac,
                                          Sexo = clte.Sexo,
                                          TelCasa = clte.TelCasa,
                                          TelCel = clte.TelCel,
                                          Correo = clte.Correo,
                                          RFC = clte.RFC
                                      }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }
        // GET: Cliente/Create
        public ActionResult CreateClte()
        {
            ViewBag.Municipio = Model.Municipio();
            return PartialView("_CreateCliente");
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult CreateClte(ClienteCreate model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Municipio = Model.Municipio();
                return PartialView("_CreateCliente", model);
            }
            else
            {
                Cliente clte = new Cliente();
                try
                {
                    clte.IdCliente = model.IdCliente;
                    clte.Nombre = model.Nombre;
                    clte.Edad = model.Edad;
                    clte.Direccion = model.Direccion;
                    clte.FechaNac = model.FechaNac;
                    clte.Sexo = model.Sexo;
                    clte.TelCasa = model.TelCasa;
                    clte.TelCel = model.TelCel;
                    clte.Correo = model.Correo;
                    clte.RFC = model.RFC;
                    clte.IdMunicipio = model.IdMunicipio;
                    db.Cliente.Add(clte);
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
        public ActionResult UpdateClte(int IdCliente)
        {
            ViewBag.Municipio = Model.Municipio();
            ClienteCreate model = (from C in db.Cliente
                                   where C.IdCliente == IdCliente
                                   select new ClienteCreate
                                   {
                                       Nombre = C.Nombre,
                                       Edad = C.Edad,
                                       Direccion = C.Direccion,
                                       FechaNac = C.FechaNac,
                                       Sexo = C.Sexo,
                                       TelCasa = C.TelCasa,
                                       TelCel = C.TelCel,
                                       Correo = C.Correo,
                                       RFC = C.RFC,
                                       IdMunicipio = C.IdMunicipio
                                   }).FirstOrDefault();
            Session["IdCliente"] = IdCliente;
            return PartialView("_UpdateCliente", model);
        }

        [HttpPost]
        public ActionResult UpdateClte(ClienteCreate model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Municipio = Model.Municipio();
                return PartialView("_UpdateCliente", model);
            }
            else
            {
                IdCliente = Convert.ToInt32(Session["IdCliente"]);
                try
                {
                    Cliente clte = (from C in db.Cliente
                                    where C.IdCliente == IdCliente
                                    select C).SingleOrDefault();
                    clte.Nombre = model.Nombre;
                    clte.Edad = model.Edad;
                    clte.Direccion = model.Direccion;
                    clte.FechaNac = model.FechaNac;
                    clte.Sexo = model.Sexo;
                    clte.TelCasa = model.TelCasa;
                    clte.TelCel = model.TelCel;
                    clte.Correo = model.Correo;
                    clte.RFC = model.RFC;
                    clte.IdMunicipio = model.IdMunicipio;
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteClte(int Id)
        {
            Cliente c = db.Cliente.Where(x => x.IdCliente == Id).FirstOrDefault();
            db.Cliente.Remove(c);
            db.SaveChanges();
            return Content("Correcto");
        }

        public ActionResult Options(int Id)
        {
            Session["IdCliente"] = Id;
            return View();
        }

        
    }        
}
