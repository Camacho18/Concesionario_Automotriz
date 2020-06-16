using Concesionaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//User
using Concesionaria.Models.ModelsSupport;

namespace Concesionaria.AutoComplete
{
    public class DropDownList
    {
        public readonly ConcesionariaEntities db = new ConcesionariaEntities();

        public List<DropDownListModel> TipoEmpleado()
        {
            return (from T in db.TipoEmpleado select new DropDownListModel { Id = T.IdTipoEmpleado, Value = T.Nombre }).ToList();
        }
        public List<DropDownListModel> Municipio()
        {
            return (from M in db.Municipio select new DropDownListModel { Id = M.IdMunicipio, Value = M.Nombre }).ToList();
        }
        public List<DropDownListModel> EstadoCliente()
        {
            return (from E in db.Estado_Cliente select new DropDownListModel { Id = E.IdEstado_Cliente, Value = E.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoEstadoList()
        {
            return (from A in db.AutoEstadoList select new DropDownListModel { Id = A.IdAutoEstado, Value = A.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoMarca()
        {
            return (from AM in db.AutoMarca select new DropDownListModel { Id = AM.IdAutoMarca, Value = AM.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoModelo()
        {
            return (from AM in db.AutoModelo select new DropDownListModel { Id = AM.IdAutoModelo, Value = AM.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoColor()
        {
            return (from AC in db.AutoColorList select new DropDownListModel { Id = AC.IdAutoColor, Value = AC.Nombre }).ToList();
        }
        public List<DropDownListModel> Anios()
        {
            return (from AC in db.Anios select new DropDownListModel { Id = AC.IdAnios, Value = AC.Numero }).ToList();
        }
        public List<DropDownListModel> Fabrica()
        {
            return (from AC in db.Fabrica select new DropDownListModel { Id = AC.IdFabrica, Value = AC.Numero+" "+ AC.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoAcce(int? IDM, int? IDA)
        {
            return (from AC in db.Accesorio 
                    join AL in db.AccesorioList on AC.IdAccesorioList equals AL.IdAccesorioList
                    where AC.Estado==true && AL.IdAutoModelo==IDM && AL.IdAnios==IDA
                    select new DropDownListModel { Id = AC.IdAccesorio, Value = AC.Serie+" - "+ AL.Numero+" - "+ AL.Nombre }).ToList();
        }
        

        public List<DropDownListModel> CategoriaAutopartes() 
        {
            return (from A in db.CategoriaAutoparte select new DropDownListModel { Id = A.IdCategoriaAutoparte, Value = A.Categoria }).ToList();
        }
        public List<DropDownListModel> MantenEstado()
        {
            return (from M in db.MantenEstado select new DropDownListModel { Id = M.IdMantenEstado, Value = M.Nombre }).ToList();
        }
        public List<DropDownListModel> Automovil()
        {
            return (from AM in db.AutoModelo join A in db.Automovil on AM.IdAutoModelo equals A.IdAutoModelo select new DropDownListModel { Id = AM.IdAutoModelo, Value = AM.Nombre }).ToList();
        }
        public List<DropDownListModel> Autopartes()
        {
            return (from a in db.Autopartes select new DropDownListModel { Id = a.IdAutopartes, Value = a.Nombre }).ToList();
        }
        public List<DropDownListModel> AutoConce(int id)
        {

            return (from a in db.Automovil
                    join m in db.AutoModelo on a.IdAutoModelo equals m.IdAutoModelo                    
                    where a.IdConcesinaria==id
                    select new DropDownListModel { Id = a.IdAutomovil, Value =a.Numero + " - " + m.Nombre }).ToList();
        }
        public List<DropDownListModel> Sucursal(int id)
        {

            return (from a in db.Concesinaria                                        
                    where a.IdConcesinaria!=id
                    select new DropDownListModel { Id = a.IdConcesinaria, Value = a.Nombre}).ToList();
        }

    }
}