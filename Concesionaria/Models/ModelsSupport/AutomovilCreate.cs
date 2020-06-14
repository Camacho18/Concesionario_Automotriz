using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutomovilCreate
    {
        public int IdFabrica { get; set; }
        public string NumeroF { get; set; }
        public string NumeroA { get; set; }
        public int? IdAnio { get; set; }
        public int? IdAutoModelo { get; set; }
        public int? IdAutoColor { get; set; }
        
    }
}