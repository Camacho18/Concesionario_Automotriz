using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class EmpleadoCreate
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Campo necesario")]
        public int? Telefono { get; set; }
        public bool C_Estado { get; set; }
        public int? IdTipoEmpleado { get; set; }
        public int? IdConcesinaria { get; set; }
    }
}