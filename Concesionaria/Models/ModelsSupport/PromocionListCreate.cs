using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class PromocionListCreate
    {
        public int IdPromocion { get; set; }
        public string Numero { get; set; }
        public Nullable<int> Cantidad_Auto { get; set; }
        public Nullable<int> Descuento { get; set; }
    }
}