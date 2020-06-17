using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoAccesorioJson
    {
        public int IdAutoAccesorio { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
    }
}