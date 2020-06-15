using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class MantenimientoJson
    {
        public int IdMantenimiento { get; set; }
        public DateTime? Fecha { get; set; }
        public string Usuario { get; set; }
        public string Automovil { get; set; }
        public string Estado { get; set; }
    }
}