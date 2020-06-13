using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class PromocionAutoJson
    {
        public int IdPromocion_Auto { get; set; }
        public string AutoModelo { get; set; }
        public string Anios { get; set; }
        public string Vigente { get; set; }
    }
}