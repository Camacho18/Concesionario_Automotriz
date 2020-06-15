using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoJson
    {
        public int IdAutomovil { get; set; }
        public string Numero { get; set; }       
        public string Modelo { get; set; }
        public string Estado { get; set; }

        public string Precio { get; set; }
        public string Adquisicion { get; set; }
        public string Fabrica { get; set; }
    }
}