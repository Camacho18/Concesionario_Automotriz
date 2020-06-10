using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class ReferenciaCreate
    {
        public int IdReferencia { get; set; }        
        public string Numero { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string TelCel { get; set; }
        public int? IdCliente { get; set; }
    }
}