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
    
    public partial class AccesorioList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccesorioList()
        {
            this.Accesorio = new HashSet<Accesorio>();
        }
    
        public int IdAccesorioList { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IdAutoModelo { get; set; }
        public Nullable<int> IdAnios { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accesorio> Accesorio { get; set; }
        public virtual Anios Anios { get; set; }
        public virtual AutoModelo AutoModelo { get; set; }
    }
}
