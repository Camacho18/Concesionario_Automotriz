using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class VentaAutoJson
    {
        public int IdVentaAuto { get; set; }
        public string Numero { get; set; }
        public string Usuario { get; set; }
        public string Cliente { get; set; }
        public string EstadoVenta { get; set; }
        public decimal? PrecioFinal { get; set; }
    }
}