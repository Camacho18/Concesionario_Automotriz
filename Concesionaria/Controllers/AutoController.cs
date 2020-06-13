using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Concesionaria.Controllers
{
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Auto/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
