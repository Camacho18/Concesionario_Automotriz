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
    
    public partial class Accesorio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accesorio()
        {
            this.AutoAccesorio = new HashSet<AutoAccesorio>();
        }
    
        public int IdAccesorio { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdAccesorioList { get; set; }
        public Nullable<int> IdConcesinaria { get; set; }
    
        public virtual AccesorioList AccesorioList { get; set; }
        public virtual Concesinaria Concesinaria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoAccesorio> AutoAccesorio { get; set; }
    }
}
