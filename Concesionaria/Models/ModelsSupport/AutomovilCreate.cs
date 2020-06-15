using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Concesionaria.Models.ModelsSupport
{
    using Concesionaria.AutoComplete;
    using Microsoft.Ajax.Utilities;
    using System.ComponentModel.DataAnnotations;


    public class AutomovilCreate
    {
        [Required]
        public int IdFabrica { get; set; }
        [Required]
        public string NumeroF { get; set; }
        [Required]
        public string NumeroA { get; set; }
        [Required]
        public int? IdAnio { get; set; }
        [Required]
        public int? IdAutoModelo { get; set; }
        [Required]
        public int? IdAutoColor { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)] 
        [ValidatinRangeDate]
        public DateTime FechaIngreso { get; set; }
        [Required]
        public Nullable<decimal> PrecioCompra { get; set; }
        [Required]
        public Nullable<decimal> PrecioVenta { get; set; }

    }
}