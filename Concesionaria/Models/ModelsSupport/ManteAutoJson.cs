using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class ManteAutoJson
    {
        public int IdManten_Autopar { get; set; }
        public decimal? Precio { get; set; }
        public int? IdMantenimiento { get; set; }
        public string Autopartes { get; set; }
        public int? Cantidad_Autopartes { get; set; }
    }
}