using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using System.ComponentModel.DataAnnotations;
    public class AutoUpdate
    {
      
        public string IdFabrica { get; set; }
        public string NumeroF { get; set; }
        public string NumeroA { get; set; }
        public string IdAnio { get; set; }
        public string IdAutoModelo { get; set; }
        public string IdAutoColor { get; set; }
        public string FechaIngreso { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
        [Required]
        public Nullable<decimal> PrecioCompra { get; set; }
        [Required]
        public Nullable<decimal> PrecioVenta { get; set; }
    }
}