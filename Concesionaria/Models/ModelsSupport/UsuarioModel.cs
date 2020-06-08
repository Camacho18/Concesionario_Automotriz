using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string NomUsuario { get; set; }
        public string Contrasena { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public bool? Acceso { get; set; }
        public bool? C_Estado { get; set; }

        
    }
}