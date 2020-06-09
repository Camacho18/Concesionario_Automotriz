using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class ClienteJson
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNac { get; set; }
        public string Sexo { get; set; }
        public int? TelCasa { get; set; }
        public int? TelCel { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }
        public int IdMunicipio { get; set; }
    }
}