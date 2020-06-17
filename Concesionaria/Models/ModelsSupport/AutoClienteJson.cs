using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoClienteJson
    {
        public int IdAutoCliente { get; set; }
        public string Numero { get; set; }
        public decimal? PrecioVenta { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Anio { get; set; }

    }
}