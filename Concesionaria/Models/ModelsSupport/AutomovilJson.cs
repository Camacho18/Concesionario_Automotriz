using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutomovilJson
    {
        public int IdAutomovil { get; set; }
        public string Numero { get; set; }
        public int? Anio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Estado { get; set; }


    }
}