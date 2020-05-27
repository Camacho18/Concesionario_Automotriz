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

    }
}