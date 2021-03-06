﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ConcesionariaEntities : DbContext
    {
        public ConcesionariaEntities()
            : base("name=ConcesionariaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accesorio> Accesorio { get; set; }
        public virtual DbSet<AccesorioList> AccesorioList { get; set; }
        public virtual DbSet<Anios> Anios { get; set; }
        public virtual DbSet<AutoAccesorio> AutoAccesorio { get; set; }
        public virtual DbSet<AutoCliente> AutoCliente { get; set; }
        public virtual DbSet<AutoColorList> AutoColorList { get; set; }
        public virtual DbSet<AutoEstadoList> AutoEstadoList { get; set; }
        public virtual DbSet<AutoMarca> AutoMarca { get; set; }
        public virtual DbSet<AutoModelo> AutoModelo { get; set; }
        public virtual DbSet<Automovil> Automovil { get; set; }
        public virtual DbSet<Autopartes> Autopartes { get; set; }
        public virtual DbSet<CategoriaAutoparte> CategoriaAutoparte { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Concesinaria> Concesinaria { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<eRROR> eRROR { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Estado_Cliente> Estado_Cliente { get; set; }
        public virtual DbSet<EstadoVenta> EstadoVenta { get; set; }
        public virtual DbSet<Fabrica> Fabrica { get; set; }
        public virtual DbSet<Manten_Autopar> Manten_Autopar { get; set; }
        public virtual DbSet<MantenEstado> MantenEstado { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Origen_Fabrica> Origen_Fabrica { get; set; }
        public virtual DbSet<Origen_Traspaso> Origen_Traspaso { get; set; }
        public virtual DbSet<OrigenEstado> OrigenEstado { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Promocion_Auto> Promocion_Auto { get; set; }
        public virtual DbSet<PromocionList> PromocionList { get; set; }
        public virtual DbSet<Referencias> Referencias { get; set; }
        public virtual DbSet<TipoEmpleado> TipoEmpleado { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<VentaAuto> VentaAuto { get; set; }
    
        public virtual int SP_Automovil_Create_Fabrica(Nullable<int> idFabrica, string numeroF, Nullable<int> idUsuario, string numeroA, Nullable<int> idAnio, Nullable<int> idAutoModelo, Nullable<int> idAutoColor, Nullable<decimal> precioCompra, Nullable<decimal> precionVenta, Nullable<System.DateTime> fecha, Nullable<int> idSuc, ObjectParameter bandera)
        {
            var idFabricaParameter = idFabrica.HasValue ?
                new ObjectParameter("IdFabrica", idFabrica) :
                new ObjectParameter("IdFabrica", typeof(int));
    
            var numeroFParameter = numeroF != null ?
                new ObjectParameter("NumeroF", numeroF) :
                new ObjectParameter("NumeroF", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var numeroAParameter = numeroA != null ?
                new ObjectParameter("NumeroA", numeroA) :
                new ObjectParameter("NumeroA", typeof(string));
    
            var idAnioParameter = idAnio.HasValue ?
                new ObjectParameter("IdAnio", idAnio) :
                new ObjectParameter("IdAnio", typeof(int));
    
            var idAutoModeloParameter = idAutoModelo.HasValue ?
                new ObjectParameter("IdAutoModelo", idAutoModelo) :
                new ObjectParameter("IdAutoModelo", typeof(int));
    
            var idAutoColorParameter = idAutoColor.HasValue ?
                new ObjectParameter("IdAutoColor", idAutoColor) :
                new ObjectParameter("IdAutoColor", typeof(int));
    
            var precioCompraParameter = precioCompra.HasValue ?
                new ObjectParameter("PrecioCompra", precioCompra) :
                new ObjectParameter("PrecioCompra", typeof(decimal));
    
            var precionVentaParameter = precionVenta.HasValue ?
                new ObjectParameter("PrecionVenta", precionVenta) :
                new ObjectParameter("PrecionVenta", typeof(decimal));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var idSucParameter = idSuc.HasValue ?
                new ObjectParameter("IdSuc", idSuc) :
                new ObjectParameter("IdSuc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Automovil_Create_Fabrica", idFabricaParameter, numeroFParameter, idUsuarioParameter, numeroAParameter, idAnioParameter, idAutoModeloParameter, idAutoColorParameter, precioCompraParameter, precionVentaParameter, fechaParameter, idSucParameter, bandera);
        }
    }
}
