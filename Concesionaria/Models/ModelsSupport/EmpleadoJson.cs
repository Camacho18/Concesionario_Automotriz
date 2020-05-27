using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class EmpleadoJson
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public int? Telefono { get; set; }
        public bool C_Estado { get; set; }
        public int IdTipoEmpleado { get; set; }
        public int IdConcesinaria { get; set; }
    }
}