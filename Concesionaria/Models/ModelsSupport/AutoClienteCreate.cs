using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoClienteCreate
    {
        public int IdAutoCliente { get; set; }
        public int IdAutomovil { get; set; }
        public int IdVentaAuto { get; set; }
        public Nullable<bool> Promo { get; set; }
    }
}