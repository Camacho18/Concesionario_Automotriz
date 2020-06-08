using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioCreate
    {
        public int IdAccesorio { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> CantidadExistencia { get; set; }
        public Nullable<int> CantidadVendido { get; set; }
        public string Numero { get; set; }
    }
}