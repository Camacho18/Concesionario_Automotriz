using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoTraspaso
    {
        [Required]
        public string Numero { get; set; }
        public int IdVendedor { get; set; }
        public int IdComprador { get; set; }
        [Required]
        public int IdConcesionaria { get; set; }
        [Required]
        public int IdAutomovil { get; set; }
    }
}