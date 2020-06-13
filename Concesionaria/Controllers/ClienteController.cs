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
        private readonly RemoveEspace Remove = new RemoveEspace();
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
            List<ClienteJson> json = (from clte in db.Cliente
                                      select new ClienteJson
                                      {
                                          IdCliente = clte.IdCliente,
                                          Numero = clte.Numero,
                                          Nombre = clte.Nombre,                                          
                                          Direccion = clte.Direccion,
                                          FechaNac = clte.FechaNac,
                                          Sexo = clte.Sexo,
                                          TelCasa = clte.TelCasa,
                                          TelCel = clte.TelCel,
                                          Correo = clte.Correo,
                                          RFC = clte.RFC,
                                          Municipio = (from M in db.Municipio where M.IdMunicipio == clte.IdMunicipio select M.Nombre).FirstOrDefault(),
                                          Estado = (from E in db.Estado_Cliente where E.IdEstado_Cliente == clte.IdEstado_Cliente select E.Nombre).FirstOrDefault()
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
                    model.Numero = Remove.RemoveAllWhitespace(model.Numero);
                    int comd = (from C in db.Cliente where C.Numero == model.Numero select C.IdEstado_Cliente).Count();

                    if (comd >= 1)
                        return Json("2", JsonRequestBehavior.AllowGet);

                    clte.Numero = model.Numero;
                    clte.Nombre = model.Nombre;                    
                    clte.Direccion = model.Direccion;
                    clte.FechaNac = model.FechaNac;
                    clte.Sexo = model.Sexo;
                    clte.TelCasa = model.TelCasa;
                    clte.TelCel = model.TelCel;
                    clte.Correo = model.Correo;
                    clte.RFC = model.RFC;
                    clte.IdMunicipio = model.IdMunicipio;
                    clte.IdEstado_Cliente = 1;
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
            ViewBag.EstadoCliente = Model.EstadoCliente();
            ClienteCreate model = (from C in db.Cliente
                                   where C.IdCliente == IdCliente
                                   select new ClienteCreate
                                   {
                                       Numero = C.Numero,
                                       Nombre = C.Nombre,                                       
                                       Direccion = C.Direccion,
                                       FechaNac = C.FechaNac,
                                       Sexo = C.Sexo,
                                       TelCasa = C.TelCasa,
                                       TelCel = C.TelCel,
                                       Correo = C.Correo,
                                       RFC = C.RFC,
                                       IdMunicipio = C.IdMunicipio,
                                       IdEstado_Cliente = C.IdEstado_Cliente
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
                    clte.Direccion = model.Direccion;
                    clte.FechaNac = model.FechaNac;
                    clte.Sexo = model.Sexo;
                    clte.TelCasa = model.TelCasa;
                    clte.TelCel = model.TelCel;
                    clte.Correo = model.Correo;
                    clte.RFC = model.RFC;
                    clte.IdMunicipio = model.IdMunicipio;
                    clte.IdEstado_Cliente = 1;
                    db.SaveChanges();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult DeleteClte(int IdCliente)
        {
            Cliente c = db.Cliente.Where(x => x.IdCliente == IdCliente).FirstOrDefault();
            c.IdEstado_Cliente = 4;
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivarClte(int IdCliente)
        {
            Cliente c = db.Cliente.Where(x => x.IdCliente == IdCliente).FirstOrDefault();
            c.IdEstado_Cliente = 1;
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Options(int Id)
        {
            Session["IdCliente"] = Id;
            ViewBag.Nombre = (from C in db.Cliente where C.IdCliente == Id select C.Nombre).FirstOrDefault();
            return View();
        }

        
    }        
}
