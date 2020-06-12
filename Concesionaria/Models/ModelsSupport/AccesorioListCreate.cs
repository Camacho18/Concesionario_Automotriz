using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AccesorioListCreate
    {
        public int IdAccesorioList { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IdAutoModelo { get; set; }
        public Nullable<int> IdAnios { get; set; }
    }
}