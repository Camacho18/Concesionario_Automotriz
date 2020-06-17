using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class InfoCliente
    {
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string FechaNac { get; set; }
        public string Sexo { get; set; }
        public string TelCasa { get; set; }
        public string TelCel { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Estado_Cliente { get; set; }
    }
}