using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class ManteAutoCreate
    {
        public int IdManten_Autopar { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<int> IdMantenimiento { get; set; }
        public Nullable<int> IdAutopartes { get; set; }
        public Nullable<int> Cantidad_Autopartes { get; set; }
    }
}