using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class EmpleadoJson
    {
        public int IdEmpleado { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public int IdConcesinaria { get; set; }
    }
}