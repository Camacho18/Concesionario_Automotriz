using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutomovilCreate
    {
        public int IdAutomovil { get; set; }
        public string Numero { get; set; }
        public int? Anio { get; set; }
        public int? IdAutoModelo { get; set; }
        public int? IdAutoColor { get; set; }
        public int? IdAutoEstado { get; set; }
    }
}