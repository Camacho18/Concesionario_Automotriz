using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class CreateAcceAuto
    {
        [Required]
        public int IdAccesorio { get; set; }
    }
}