using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class ClienteCreate
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public Nullable<int> Edad { get; set; }
        public string Direccion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> FechaNac { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> TelCasa { get; set; }
        public Nullable<int> TelCel { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }
        public Nullable<int> IdMunicipio { get; set; }
    }
}