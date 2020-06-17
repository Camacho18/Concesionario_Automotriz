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
    
    public partial class VentaAuto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VentaAuto()
        {
            this.AutoCliente = new HashSet<AutoCliente>();
        }
    
        public int IdVentaAuto { get; set; }
        public string Numero { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdEstadoVenta { get; set; }
        public Nullable<decimal> PrecioFinal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoCliente> AutoCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual EstadoVenta EstadoVenta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
