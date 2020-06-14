using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class PromocionListCreate
    {
        public int IdPromocion { get; set; }
        public string Numero { get; set; }
        public Nullable<int> Cantidad_Auto { get; set; }
        public Nullable<int> Descuento { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> FechaVigencia { get; set; }
    }
}