using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using System.ComponentModel.DataAnnotations;
    public class Login
    {
        [Required(ErrorMessage = "Ingresa Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingresa Contraseña")]
        [DataType(DataType.Password)]
        public string Contras { get; set; }
    }
}