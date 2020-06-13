using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class PromocionAutoCreate
    {
        public int IdPromocion_Auto { get; set; }
        public Nullable<int> IdAutoModelo { get; set; }
        public Nullable<int> IdAnios { get; set; }
        public Nullable<int> IdPromocion { get; set; }
        public Nullable<int> IdConcesinaria { get; set; }
        public Nullable<bool> Vigente { get; set; }
    }
}