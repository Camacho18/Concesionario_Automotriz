using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutopartesJson
    {
        public int IdAutopartes { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public int IdConcesinaria { get; set; }
        public int? Cantidad { get; set; }


    }
}