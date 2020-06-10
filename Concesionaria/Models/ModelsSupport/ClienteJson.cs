using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class ClienteJson
    {
        public int IdCliente { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        
        public string Direccion { get; set; }
        public DateTime? FechaNac { get; set; }
        public string Sexo { get; set; }
        public string TelCasa { get; set; }
        public string TelCel { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }

    }
}