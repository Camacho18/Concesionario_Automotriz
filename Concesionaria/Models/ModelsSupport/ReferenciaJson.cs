﻿
namespace Concesionaria.Models.ModelsSupport
{
    public class ReferenciaJson
    {
        public int IdReferencia { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string TelCel { get; set; }
        public int? IdCliente { get; set; }
    }
}