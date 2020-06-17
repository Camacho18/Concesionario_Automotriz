using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class PromocionListJson
    {
        public int IdPromocion { get; set; }
        public string Numero { get; set; }
        public int? Cantidad_Auto { get; set; }
        public int? Descuento { get; set; }
        public DateTime? FechaVigencia { get; set; }

        public string Tipo { get; set; }
    }
}