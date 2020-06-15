using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutopartesCreate
    {
        public int IdAutopartes { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdCategoriaAutoparte { get; set; }
        public Nullable<int> IdConcesinaria { get; set; }
        public Nullable<int> Cantidad { get; set; }


    }
}