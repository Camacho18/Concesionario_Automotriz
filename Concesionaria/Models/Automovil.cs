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
    
    public partial class Automovil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Automovil()
        {
            this.AutoAccesorio = new HashSet<AutoAccesorio>();
            this.AutoCliente = new HashSet<AutoCliente>();
            this.Origen_Fabrica = new HashSet<Origen_Fabrica>();
            this.Origen_Traspaso = new HashSet<Origen_Traspaso>();
        }
    
        public int IdAutomovil { get; set; }
        public string Numero { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<int> IdAutoModelo { get; set; }
        public Nullable<int> IdAutoColor { get; set; }
        public Nullable<int> IdAutoEstado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoAccesorio> AutoAccesorio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoCliente> AutoCliente { get; set; }
        public virtual AutoColorList AutoColorList { get; set; }
        public virtual AutoEstadoList AutoEstadoList { get; set; }
        public virtual AutoModelo AutoModelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Origen_Fabrica> Origen_Fabrica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Origen_Traspaso> Origen_Traspaso { get; set; }
    }
}