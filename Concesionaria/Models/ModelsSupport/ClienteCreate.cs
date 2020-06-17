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
        public string Numero { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<DateTime> FechaNac { get; set; }
        public string Sexo { get; set; }
        public string TelCasa { get; set; }
        public string TelCel { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }
        public Nullable<int> IdMunicipio { get; set; }
        public int? IdEstado_Cliente { get; set; }

    }
}