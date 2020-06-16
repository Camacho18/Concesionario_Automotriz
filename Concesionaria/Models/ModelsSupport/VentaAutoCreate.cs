using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class VentaAutoCreate
    {
        public int IdVentaAuto { get; set; }
        public string Numero { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdEstadoVenta { get; set; }
    }
}