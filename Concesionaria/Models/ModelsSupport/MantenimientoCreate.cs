using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    public class MantenimientoCreate
    {
        public int IdMantenimiento { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdAutomovil { get; set; }
        public Nullable<int> IdMantenEstado { get; set; }
    }
}