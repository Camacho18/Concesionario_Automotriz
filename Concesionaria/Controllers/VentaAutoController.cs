﻿using System;
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
        //private readonly RemoveEspace Remove = new RemoveEspace();
        private int IdUsuario, IdSucursal;
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
                                        select new VentaAutoJson {
                                            IdVentaAuto = V.IdVentaAuto,
                                            Usuario = (from U in db.Usuario where U.IdUsuario == V.IdUsuario select U.NomUsuario).FirstOrDefault(),
                                            NumPromocion = (from P in db.PromocionList where P.IdPromocion == V.IdPromocion select P.Numero).FirstOrDefault(),
                                            Descuento = (from P in db.PromocionList where P.IdPromocion == V.IdPromocion select P.Descuento).FirstOrDefault(),
                                            Cliente = (from C in db.Cliente where C.IdCliente == V.IdCliente select C.Nombre).FirstOrDefault(),
                                            Estado = (from E in db.EstadoVenta where E.IdEstadoVenta==V.IdEstadoVenta select E.Nombre).FirstOrDefault()
                                        }).ToList();        
            JsonString = JsonConvert.SerializeObject(json);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        // GET: VentaAuto/Create
        public ActionResult CreateVentaAuto()
        {
            ViewBag.Promocion = Model.Promocion();
            ViewBag.Cliente = Model.Cliente();
            return PartialView("_CreateVentaAuto");
        }

        // POST: VentaAuto/Create
        [HttpPost]
        public ActionResult CreateVentaAuto(VentaAutoCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Promocion = Model.Promocion();
                ViewBag.Cliente = Model.Cliente();
                return PartialView("_CreateVentaauto");
            }
            else
            {
                VentaAuto m = new VentaAuto();
                
                try
                {                                       
                    m.IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    m.IdPromocion = model.IdPromocion;
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

        // GET: VentaAuto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VentaAuto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //----------------------------------------AUTOCLIENTE
        public ActionResult AutoClienteJson(int? IdVentaAuto)
        {
            if (IdVentaAuto == null)
                IdVentaAuto = Convert.ToInt32(Session["IdVentaAuto"]);
            else
                Session["IdVentaAuto"] = IdVentaAuto;

            List<AutoClienteJson> json = (from V in db.AutoCliente
                                          where V.IdVentaAuto ==IdVentaAuto
                                        select new AutoClienteJson
                                        {
                                            IdAutoCliente = V.IdAutoCliente,
                                            Numero = (from A in db.Automovil where A.IdAutomovil == V.IdAutomovil select A.Numero).FirstOrDefault(),
                                            PrecioVenta = (from A in db.Automovil where A.IdAutomovil == V.IdAutomovil select A.PrecioVenta).FirstOrDefault(),
                                            Marca = (from P in db.Automovil join Ma in db.AutoModelo on P.IdAutoModelo equals Ma.IdAutoModelo join M in db.AutoMarca on Ma.IdAutoMarca equals M.IdAutoMarca where P.IdAutomovil==V.IdAutomovil select M.Nombre).FirstOrDefault(),
                                            Modelo = (from P in db.Automovil join Ma in db.AutoModelo on P.IdAutoModelo equals Ma.IdAutoModelo where P.IdAutomovil == V.IdAutomovil select Ma.Nombre).FirstOrDefault(),
                                            Color = (from P in db.Automovil join C in db.AutoColorList on P.IdAutoColor equals C.IdAutoColor where P.IdAutomovil==V.IdAutomovil select C.Nombre).FirstOrDefault(),
                                            Anio = (from P in db.Automovil join C in db.Anios on P.IdAnios equals C.IdAnios where P.IdAutomovil == V.IdAutomovil select C.Numero).FirstOrDefault()
                                        }).ToList();
            JsonString = JsonConvert.SerializeObject(json);
            var num = (from v in db.VentaAuto join c in db.Cliente on v.IdCliente equals c.IdCliente where v.IdVentaAuto== IdVentaAuto select c.Nombre).FirstOrDefault();
            return Json(new { JsonString, num }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAutoCliente()
        {
            ViewBag.Automovil = Model.Automovil();
            return PartialView("_CreateAutoCliente");
        }

        [HttpPost]
        public ActionResult CreateAutoCliente(AutoClienteCreate model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Automovil = Model.Automovil();
                return PartialView("_CreateAutoCliente", model);
            }
            else
            {
                AutoCliente modeladd = new AutoCliente();
                int IdVentaAuto = Convert.ToInt32(Session["IdVentaAuto"]);
                try
                {
                    modeladd.IdAutomovil = model.IdAutomovil;
                    modeladd.IdVentaAuto = IdVentaAuto;
                    db.AutoCliente.Add(modeladd);
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