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
    public class MenuController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList Model = new DropDownList();
        private int Id;

        // GET: Menu
        public ActionResult MenuHorizontal()
        {
            return View("_MenuHorizontal");
        }

        // GET: Menu/Details/5
        public ActionResult VistaHorizontal()
        {
            Id = Convert.ToInt32(Session["IdUsuario"]);       
            ViewBag.Usuario = (from U in db.Usuario where U.IdUsuario == Id select U.NomUsuario).FirstOrDefault();
            Id = Convert.ToInt32(Session["IdSucursal"]);
            ViewBag.NombreCon = (from S in db.Concesinaria where S.IdConcesinaria == Id select S.Nombre).FirstOrDefault();
            return View("_VistaHorizontal");
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
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

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
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
    }
}
