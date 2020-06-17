using Concesionaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//User 
using Concesionaria.Models.ModelsSupport;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Concesionaria.AutoComplete;

namespace Concesionaria.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ConcesionariaEntities db = new ConcesionariaEntities();
        private readonly DropDownList DropDownLisObject = new DropDownList();
        private readonly RemoveEspace Remove = new RemoveEspace();
        private string JsonString = string.Empty;
        private int IdUsuario, IdSucursal, IdEmpleado, IdCom;

        // GET: Reportes
        public ActionResult FacturaReport()
        {
            IdSucursal = Convert.ToInt32(Session["IdSucursal"]);
            ViewBag.NameSuc = (from s in db.Concesinaria where s.IdConcesinaria == IdSucursal select s.Nombre).FirstOrDefault();

            List<AccesorioList> m = (from a in db.AccesorioList select a).ToList();
            return View(m);
        }        
    }
}
