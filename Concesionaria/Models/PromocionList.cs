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
    
    public partial class PromocionList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PromocionList()
        {
            this.Promocion_Auto = new HashSet<Promocion_Auto>();
        }
    
        public int IdPromocion { get; set; }
        public string Numero { get; set; }
        public Nullable<int> Cantidad_Auto { get; set; }
        public Nullable<int> Descuento { get; set; }
        public Nullable<System.DateTime> FechaVigencia { get; set; }
        public Nullable<bool> Tipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promocion_Auto> Promocion_Auto { get; set; }
    }
}
