using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concesionaria.Models.ModelsSupport
{
    public class AutoAccesorioCreate
    {
        public int IdAutoAccesorio { get; set; }
        public Nullable<int> IdAccesorio { get; set; }
        public Nullable<int> IdAutomovil { get; set; }
    }
}