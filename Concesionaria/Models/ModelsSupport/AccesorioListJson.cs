using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioListJson
    {
        public int IdAccesorioList { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string IdAutoModelo { get; set; }
        public string IdAnios { get; set; }
    }
}