//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Concesionaria.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AutoCliente
    {
        public int IdAutoCliente { get; set; }
        public Nullable<int> IdAutomovil { get; set; }
        public Nullable<int> IdVentaAuto { get; set; }
        public Nullable<int> IdPromocion_Auto { get; set; }
        public Nullable<bool> Promo { get; set; }
    
        public virtual Automovil Automovil { get; set; }
        public virtual Promocion_Auto Promocion_Auto { get; set; }
        public virtual VentaAuto VentaAuto { get; set; }
    }
}
