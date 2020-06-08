using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioJson
    {
        public int IdAccesorio { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> CantidadExistencia { get; set; }
        public Nullable<int> CantidadVendido { get; set; }
        public Nullable<int> IdConcesinaria { get; set; }
        public string Numero { get; set; }
    }
}