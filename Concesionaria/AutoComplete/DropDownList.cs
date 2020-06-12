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

    }
}