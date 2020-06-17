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
    public class VentaAutoController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, Comodin;
        // GET: VentaAuto
        public ActionResult Index()
        {
            IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            if (IdUsuario != 0 && IdSucursal != 0)
                return View();
            return RedirectToAction("Deslog", "Login");
        }

        // GET: VentaAuto/Details/5
        public ActionResult VentaAutoJson()
        {           
            List<VentaAutoJson> json = (from V in db.VentaAuto
<<<<<<< HEAD
                                        select new VentaAutoJson { 
                                            IdVentaAuto = V.IdVentaAuto,
=======
                                        select new VentaAutoJson {  
                                            IdVentaAuto=V.IdVentaAuto,
>>>>>>> Camacho
                                            Numero = V.Numero,
                                            Usuario = (from U in db.Usuario where U.IdUsuario == V.IdUsuario select U.NomUsuario).FirstOrDefault(),                                        
                                            Cliente = (from C in db.Cliente where C.IdCliente == V.IdCliente select C.Nombre).FirstOrDefault(),
                                            EstadoVenta = (from E in db.EstadoVenta where E.IdEstadoVenta==V.IdEstadoVenta select E.Nombre).FirstOrDefault()
                                        }).ToList();        
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: VentaAuto/Create
        public ActionResult CreateVentaAuto()
        {
            ViewBag.Cliente = Model.Cliente();
            return PartialView("_CreateVentaAuto");
        }

        // POST: VentaAuto/Create
        [HttpPost]
        public ActionResult CreateVentaAuto(VentaAutoCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cliente = Model.Cliente();
                return PartialView("_CreateVentaauto");
            }
            else
            {
                VentaAuto m = new VentaAuto();
                
                try
                {
                    m.Numero = model.Numero;
                    m.IdUsuario = Convert.ToInt32(Session["IdUsuario"]);                    
                    m.IdCliente = model.IdCliente;
                    m.IdEstadoVenta = 1;
                    db.VentaAuto.Add(m);
                    db.SaveChanges();

                    List<Cliente> Clte = (from cl in db.Cliente where cl.IdCliente == m.IdCliente select cl).ToList();
                    foreach (var  clte in Clte)
                    {
                        if (m.IdCliente == clte.IdCliente)
                        {
                            clte.IdEstado_Cliente = 2;
                            db.SaveChanges();
                            return Json("1", JsonRequestBehavior.AllowGet);
                        }
                    }
                    
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: VentaAuto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VentaAuto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CancelarVenta(int IdVentaAuto)
        {
            try
            {
                VentaAuto m = db.VentaAuto.Where(x => x.IdVentaAuto == IdVentaAuto).Select(x => x).FirstOrDefault();
                if (m.IdEstadoVenta == 1)
                {
                    m.IdEstadoVenta = 3;
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        //----------------------------------------AUTOCLIENTE

        public ActionResult Options(int Id)
        {
            Session["IdVentaAuto"] = Id;
            ViewBag.Numero = (from V in db.VentaAuto where V.IdVentaAuto== Id select V.Numero).FirstOrDefault();
            return PartialView("MenuVenta");
        }

        public ActionResult InfoCliente()
        {
            Comodin = Convert.ToInt32(Session["IdVentaAuto"]);
            InfoCliente i = (from C in db.Cliente
                             join V in db.VentaAuto on C.IdCliente equals V.IdCliente
                             where V.IdVentaAuto == Comodin
                             select new InfoCliente
                             {
                                 Numero = C.Numero,
                                 Nombre = C.Nombre,
                                 Direccion = C.Direccion,
                                 FechaNac = C.FechaNac.ToString(),
                                 Sexo = C.Sexo,
                                 TelCasa = C.TelCasa,
                                 TelCel = C.TelCel,
                                 Correo = C.Correo,
                                 RFC = C.RFC,
                                 Municipio = (from M in db.Municipio where M.IdMunicipio == C.IdMunicipio select M.Nombre).FirstOrDefault(),
                                 Estado = (from E in db.Estado join M in db.Municipio on E.IdEstado equals M.IdEstado where M.IdMunicipio == C.IdMunicipio select E.Nombre).FirstOrDefault(),
                                 Pais = (from P in db.Pais join E in db.Estado on P.IdPais equals E.IdPais join M in db.Municipio on E.IdEstado equals M.IdEstado where M.IdMunicipio == C.IdMunicipio select P.Nombre).FirstOrDefault(),
                                 Estado_Cliente = (from E in db.Estado_Cliente where E.IdEstado_Cliente == C.IdEstado_Cliente select E.Nombre).FirstOrDefault()
                             }).FirstOrDefault();
            return View("_InfoCliente", i);
        }
    }
}
