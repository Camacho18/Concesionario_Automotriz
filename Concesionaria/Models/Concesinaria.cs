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
    
    public partial class Concesinaria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Concesinaria()
        {
            this.Accesorio = new HashSet<Accesorio>();
            this.Empleado = new HashSet<Empleado>();
            this.Origen_Traspaso = new HashSet<Origen_Traspaso>();
            this.Promocion_Auto = new HashSet<Promocion_Auto>();
        }
    
        public int IdConcesinaria { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> IdMunicipio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accesorio> Accesorio { get; set; }
        public virtual Municipio Municipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleado> Empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Origen_Traspaso> Origen_Traspaso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promocion_Auto> Promocion_Auto { get; set; }
    }
}
