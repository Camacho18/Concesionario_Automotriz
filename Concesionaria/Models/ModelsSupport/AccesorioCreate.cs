using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioCreate
    {
        public int IdAccesorio { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdAccesorioList { get; set; }
        public Nullable<int> IdConcesinaria { get; set; }
        public decimal Precio { get; set; }
    }
}