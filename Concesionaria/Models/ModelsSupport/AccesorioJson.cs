using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioJson
    {
        public int IdAccesorio { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public decimal? Precio { get; set; }

    }
}