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
    public class AutoController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList DropDownLisObject = new DropDownList();
        private readonly RemoveEspace RemovesEspecesrr = new RemoveEspace();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado;
        // GET: Auto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Auto/Details/5
        public ActionResult CreateAuto()
        {
            ViewBag.IdFabrica = DropDownLisObject.Fabrica();
            ViewBag.IdModelo = DropDownLisObject.AutoModelo();
            ViewBag.IdColor = DropDownLisObject.AutoColor();
            ViewBag.IdAnio = DropDownLisObject.Anios();
            return View("_Create");
        }

        // GET: Auto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auto/Create
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

        // GET: Auto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auto/Edit/5
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

        // GET: Auto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auto/Delete/5
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
